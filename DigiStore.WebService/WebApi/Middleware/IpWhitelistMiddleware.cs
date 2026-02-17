using System.Net;

namespace DigiStore.WebService.WebApi.Middleware;

public class IpWhitelistMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<IpWhitelistMiddleware> _logger;
    private readonly HashSet<string> _allowedIps;

    public IpWhitelistMiddleware(
        RequestDelegate next,
        ILogger<IpWhitelistMiddleware> logger,
        IConfiguration configuration)
    {
        _next = next;
        _logger = logger;

        // Support both array format (appsettings.json) and semicolon-separated format (web.config)
        var allowedIps = configuration.GetSection("IpWhitelist:AllowedIPs").Get<string[]>();

        if (allowedIps == null || allowedIps.Length == 0)
        {
            // Try reading as a single semicolon-separated string (web.config format)
            var ipString = configuration["IpWhitelist:AllowedIPs"];
            allowedIps = string.IsNullOrEmpty(ipString) 
                ? Array.Empty<string>() 
                : ipString.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        _allowedIps = new HashSet<string>(allowedIps, StringComparer.OrdinalIgnoreCase);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var remoteIp = context.Connection.RemoteIpAddress;

        if (remoteIp == null)
        {
            _logger.LogWarning("Request rejected: Unable to determine remote IP address");
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            await context.Response.WriteAsync("Access denied: Unable to determine IP address.");
            return;
        }

        // Handle IPv4-mapped IPv6 addresses (::ffff:x.x.x.x)
        var ipAddress = remoteIp.IsIPv4MappedToIPv6
            ? remoteIp.MapToIPv4().ToString()
            : remoteIp.ToString();

        // Allow localhost for development
        if (IPAddress.IsLoopback(remoteIp) || _allowedIps.Contains(ipAddress))
        {
            await _next(context);
            return;
        }

        _logger.LogWarning("Access denied for IP: {IpAddress}", ipAddress);
        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        await context.Response.WriteAsync($"Access denied: IP address {ipAddress} is not allowed.");
    }
}
