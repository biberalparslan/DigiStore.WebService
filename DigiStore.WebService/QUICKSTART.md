# DigiStore API - Quick Start Guide

## ?? Quick Deployment Steps

### Step 1: Build the Release
```bash
# Windows
deploy.bat

# Linux/Mac
chmod +x deploy.sh
./deploy.sh

# Or manually
dotnet publish -c Release -o ./publish
```

### Step 2: ?? Configure Connection String Securely

**IMPORTANT: Do NOT store connection strings in JSON files!**

Choose one of these secure methods:

#### Option A: Environment Variable (Recommended)
```powershell
# Windows - Run setup script
.\setup-connection-windows.ps1

# Linux - Run setup script
chmod +x setup-connection-linux.sh
sudo ./setup-connection-linux.sh
```

#### Option B: Azure Key Vault (Best for Cloud)
```powershell
# Run Azure setup script
.\setup-azure-keyvault.ps1
```

#### Option C: Docker Environment
```bash
# Create .env file from template
cp .env.example .env
# Edit .env and set DIGISTORE_CONNECTION_STRING
```

**See `CONNECTION-STRING-QUICKREF.md` for detailed instructions**

### Step 3: Configure Other Production Settings
Edit `appsettings.Production.json` (connection string already secured above):
```json
{
  "AllowedOrigins": [
    "https://yourdomain.com",      // Replace with your domain
    "https://www.yourdomain.com"
  ]
}
```

### Step 4: Choose Deployment Method

#### Option A: IIS (Windows Server)
1. Install .NET 8 Hosting Bundle: https://dotnet.microsoft.com/download/dotnet/8.0
2. Copy `publish` folder to server (e.g., `C:\inetpub\wwwroot\digistore`)
3. Set connection string: Run `setup-connection-windows.ps1`
4. Create IIS website:
   - Physical path: `C:\inetpub\wwwroot\digistore`
   - Binding: https on port 443
   - Host name: `api.yourdomain.com`
   - Add SSL certificate
5. Done! Access: `https://api.yourdomain.com/swagger`

#### Option B: Linux + Nginx
1. Copy publish folder: `scp -r ./publish user@server:/var/www/digistore`
2. Set connection string: Run `setup-connection-linux.sh`
3. Configure Nginx reverse proxy (see DEPLOYMENT.md)
4. Start service: `sudo systemctl start digistore.service`
5. Done! Access: `https://api.yourdomain.com/swagger`

#### Option C: Docker
```bash
# Create .env file with connection string
cp .env.example .env
# Edit .env and add your connection string

# Run with docker-compose
docker-compose up -d

# Or build and run manually
docker build -t digistore-api .
docker run -d -p 80:80 -p 443:443 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e DIGISTORE_CONNECTION_STRING="Server=myserver;..." \
  --name digistore-api \
  digistore-api
```

#### Option D: Azure App Service
1. Right-click project in Visual Studio ? Publish
2. Select Azure ? Azure App Service
3. Create/Select App Service
4. Set connection string in Azure Portal or use Key Vault
5. Publish!

### Step 5: Configure DNS
Point your domain to the server:
```
A Record: api.yourdomain.com ? YOUR_SERVER_IP
```

### Step 6: SSL Certificate
- **Free**: Use Let's Encrypt (certbot)
- **Paid**: Purchase from SSL provider
- **Azure**: Use built-in SSL binding

### Step 7: Test Deployment
```bash
# Health check
curl https://api.yourdomain.com/api/health

# Swagger UI
https://api.yourdomain.com/swagger
```

## ?? Files Created for You

### Security & Configuration
- ? `appsettings.Production.json` - Production configuration (NO connection string)
- ? `setup-connection-windows.ps1` - Windows connection string setup
- ? `setup-connection-linux.sh` - Linux connection string setup
- ? `setup-azure-keyvault.ps1` - Azure Key Vault setup
- ? `CONNECTION-STRING-QUICKREF.md` - Quick reference for connection strings
- ? `SECURE-CONNECTION-STRING.md` - Complete security guide
- ? `.env.example` - Environment variable template

### Deployment Files
- ? `web.config` - IIS configuration
- ? `Dockerfile` - Docker containerization
- ? `docker-compose.yml` - Docker Compose setup
- ? `deploy.bat` - Windows deployment script
- ? `deploy.sh` - Linux deployment script
- ? `DEPLOYMENT.md` - Full deployment guide

### Application
- ? `HealthController.cs` - Health check endpoint
- ? Updated `Program.cs` - Production-ready with Key Vault support
- ? Updated `ServiceExtensions.cs` - Secure connection string loading

## ?? Security Checklist

Before going live:
- [ ] ? Connection string stored securely (NOT in JSON files)
- [ ] Update `AllowedOrigins` in `appsettings.Production.json` with your actual domain(s)
- [ ] Enable HTTPS with valid SSL certificate
- [ ] Verify `.gitignore` excludes sensitive files (.env, secrets)
- [ ] Configure firewall rules
- [ ] Set up regular database backups
- [ ] Test health endpoint

## ?? Monitoring

Check logs:
- **Location**: `logs/` folder
- **File format**: `digistore-YYYYMMDD.txt`
- **Level**: Warning and above in production

Health endpoint:
```
GET https://api.yourdomain.com/api/health
```

## ?? Connection String Security

Your application supports these secure methods (in priority order):

1. **Environment Variable** `DIGISTORE_CONNECTION_STRING` ? Recommended
2. **Azure Key Vault** (set `KeyVaultName` in config)
3. **User Secrets** (development only)
4. **appsettings.json** (fallback - not for production)

**Quick setup:**
- Windows: `.\setup-connection-windows.ps1`
- Linux: `sudo ./setup-connection-linux.sh`
- Azure: `.\setup-azure-keyvault.ps1`
- Docker: Edit `.env` file

**See `CONNECTION-STRING-QUICKREF.md` for all methods**

## ?? Need Help?

- Connection strings: `CONNECTION-STRING-QUICKREF.md`
- Full security guide: `SECURE-CONNECTION-STRING.md`
- Deployment details: `DEPLOYMENT.md`

## ?? What's Next?

1. Set up CI/CD pipeline (GitHub Actions, Azure DevOps)
2. Configure monitoring (Application Insights, ELK)
3. Set up automated backups
4. Implement rate limiting
5. Add API versioning
6. Set up staging environment
