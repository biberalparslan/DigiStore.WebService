using Azure.Identity;
using DigiStore.WebService.WebApi.Extensions;
using DigiStore.WebService.WebApi.Middleware;
using Microsoft.Extensions.FileProviders;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure Azure Key Vault (if available)
var keyVaultName = builder.Configuration["KeyVaultName"];
if (!string.IsNullOrEmpty(keyVaultName))
{
    var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
    
    builder.Configuration.AddAzureKeyVault(
        keyVaultUri,
        new DefaultAzureCredential());
    
    Log.Information($"Azure Key Vault configured: {keyVaultName}");
}

// Serilog configuration
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/digistore-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);
});

// Add layers
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// CORS - Configure for production
builder.Services.AddCors(options =>
{
    options.AddPolicy("ProductionPolicy", policy =>
    {
        var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? new[] { "*" };
        if (allowedOrigins.Contains("*"))
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        }
        else
        {
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        }
    });
});

var app = builder.Build();

// Redirect root to Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// Always enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("ProductionPolicy");

app.UseMiddleware<ExceptionMiddleware>();

// ---------------------------------------------
// STATIC FILES for category + urunler
// ---------------------------------------------
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "category")),
    RequestPath = "/category"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "urunler")),
    RequestPath = "/urunler"
});
// ---------------------------------------------

app.UseAuthorization();

app.MapControllers();

app.Run();

// Make the WebApplication accessible for tests
public partial class Program { }
