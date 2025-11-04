# DigiStore WebService Deployment Guide

## Prerequisites
- .NET 8 Runtime installed on the server
- IIS with ASP.NET Core Hosting Bundle (for Windows IIS)
- SQL Server database
- Domain name configured and pointing to your server

## Deployment Steps

### 1. Build Release Version
Run this command in the project directory:
```bash
dotnet publish -c Release -o ./publish
```

### 2. Configure Production Settings
Update `appsettings.Production.json` with:
- **ConnectionStrings**: Update with your production database connection string
- **AllowedOrigins**: Replace with your actual domain(s)
- **Other settings**: API keys, third-party service URLs, etc.

### 3. IIS Deployment (Windows Server)

#### Install ASP.NET Core Hosting Bundle
Download and install from: https://dotnet.microsoft.com/download/dotnet/8.0

#### Create IIS Site
1. Open IIS Manager
2. Right-click "Sites" ? "Add Website"
3. Site name: `DigiStoreAPI`
4. Physical path: Point to your publish folder
5. Binding:
   - Type: https
   - IP address: All Unassigned
   - Port: 443
   - Host name: api.yourdomain.com
   - SSL certificate: Select/install your SSL certificate

#### Application Pool Settings
1. Right-click Application Pool ? Advanced Settings
2. Set ".NET CLR Version" to "No Managed Code"
3. Set "Start Mode" to "AlwaysRunning" (optional, for better performance)

### 4. Linux Deployment (with Nginx)

#### Install .NET 8 Runtime
```bash
wget https://dot.net/v1/dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 8.0 --runtime aspnetcore
```

#### Create systemd Service
Create `/etc/systemd/system/digistore.service`:
```ini
[Unit]
Description=DigiStore Web API
After=network.target

[Service]
WorkingDirectory=/var/www/digistore
ExecStart=/usr/bin/dotnet /var/www/digistore/DigiStore.WebService.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=digistore-api
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

Enable and start the service:
```bash
sudo systemctl enable digistore.service
sudo systemctl start digistore.service
```

#### Configure Nginx
Create `/etc/nginx/sites-available/digistore`:
```nginx
server {
    listen 80;
    listen [::]:80;
    server_name api.yourdomain.com;
    return 301 https://$server_name$request_uri;
}

server {
    listen 443 ssl http2;
    listen [::]:443 ssl http2;
    server_name api.yourdomain.com;

    ssl_certificate /etc/ssl/certs/yourdomain.crt;
    ssl_certificate_key /etc/ssl/private/yourdomain.key;

    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

Enable the site:
```bash
sudo ln -s /etc/nginx/sites-available/digistore /etc/nginx/sites-enabled/
sudo nginx -t
sudo systemctl reload nginx
```

### 5. Docker Deployment (Optional)

See `Dockerfile` for containerized deployment.

### 6. Azure App Service Deployment

#### Using Visual Studio
1. Right-click project ? Publish
2. Target: Azure
3. Specific target: Azure App Service (Windows/Linux)
4. Create new or select existing App Service
5. Click Publish

#### Using Azure CLI
```bash
az login
az webapp up --name digistore-api --resource-group MyResourceGroup --plan MyAppServicePlan
```

### 7. Post-Deployment Configuration

#### DNS Configuration
Point your domain to the server:
- A Record: `api.yourdomain.com` ? `YOUR_SERVER_IP`
- Or CNAME: `api.yourdomain.com` ? `your-app.azurewebsites.net` (for Azure)

#### SSL Certificate
- Use Let's Encrypt (free): https://letsencrypt.org/
- Or purchase from a Certificate Authority
- For Azure: Use built-in SSL binding or Azure Key Vault

#### Firewall Rules
- Open ports 80 (HTTP) and 443 (HTTPS)
- Configure SQL Server firewall to allow application server

### 8. Verify Deployment

Test your API:
```bash
# Health check (if you have one)
curl https://api.yourdomain.com/health

# Swagger UI (if enabled)
https://api.yourdomain.com/swagger
```

### 9. Monitoring and Maintenance

- Check logs in `logs/` folder
- Monitor application pool/service status
- Set up Application Insights (Azure) or ELK stack for monitoring
- Regular database backups
- Keep .NET runtime updated

## Security Checklist
- ? HTTPS enabled with valid SSL certificate
- ? CORS configured with specific origins (not "*")
- ? Connection strings stored securely (Azure Key Vault, environment variables)
- ? Sensitive data not in appsettings.json
- ? Firewall rules configured
- ? Regular security updates

## Troubleshooting

### Application doesn't start
- Check Event Viewer (Windows) or `journalctl -u digistore.service` (Linux)
- Verify .NET 8 runtime is installed
- Check file permissions

### 502 Bad Gateway (Nginx)
- Verify the application is running: `sudo systemctl status digistore.service`
- Check if port 5000 is listening: `netstat -tlnp | grep 5000`

### Database connection issues
- Verify connection string in appsettings.Production.json
- Check SQL Server firewall rules
- Test connection from server

## Additional Resources
- ASP.NET Core Hosting: https://docs.microsoft.com/aspnet/core/host-and-deploy/
- IIS Deployment: https://docs.microsoft.com/aspnet/core/host-and-deploy/iis/
- Linux Deployment: https://docs.microsoft.com/aspnet/core/host-and-deploy/linux-nginx
