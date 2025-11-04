# ?? Connection String Security - Quick Reference

## ?? IMPORTANT
**NEVER store production connection strings in JSON files!**

Your application now supports secure connection string storage.

---

## ?? Choose Your Method

### 1?? Environment Variables (Recommended for Most)

**Windows (IIS):**
```powershell
# Run the setup script (easiest)
.\setup-connection-windows.ps1

# Or manually set via PowerShell (as Admin)
[System.Environment]::SetEnvironmentVariable(
    'DIGISTORE_CONNECTION_STRING',
    'Server=myserver;Database=mydb;User=sa;Password=pass123',
    [System.EnvironmentVariableTarget]::Machine
)
iisreset
```

**Linux (systemd):**
```bash
# Run the setup script (easiest)
chmod +x setup-connection-linux.sh
sudo ./setup-connection-linux.sh

# Or manually edit /etc/systemd/system/digistore.service
# Add this line under [Service]:
Environment=DIGISTORE_CONNECTION_STRING=Server=myserver;Database=mydb;User=sa;Password=pass123
```

**Docker:**
```bash
# Using environment variable
docker run -e DIGISTORE_CONNECTION_STRING="Server=myserver;..." digistore-api

# Or use .env file
cp .env.example .env
# Edit .env and set DIGISTORE_CONNECTION_STRING
docker-compose up -d
```

---

### 2?? Azure Key Vault (Best for Azure)

```powershell
# Run the setup script
.\setup-azure-keyvault.ps1

# Or manually:
az keyvault create --name mykeyvault --resource-group mygroup
az keyvault secret set --vault-name mykeyvault --name "ConnectionStrings--DefaultConnection" --value "Server=..."

# Add to appsettings.json or App Service config:
"KeyVaultName": "mykeyvault"
```

---

### 3?? User Secrets (Development Only)

```bash
cd DigiStore.WebService
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=DigiStore;Integrated Security=true;"
```

---

## ?? How It Works

Your app checks for connection strings in this order:

1. **Environment Variable** `DIGISTORE_CONNECTION_STRING` ? (highest priority)
2. **Azure Key Vault** (if KeyVaultName is configured)
3. **User Secrets** (development)
4. **appsettings.json** (fallback - not recommended)

---

## ? Verification

### Check if environment variable is set:

**Windows PowerShell:**
```powershell
$env:DIGISTORE_CONNECTION_STRING
```

**Linux:**
```bash
echo $DIGISTORE_CONNECTION_STRING
```

### Check application logs:
On startup, you'll see:
```
Using connection string from environment variable
```

### Test the API:
```bash
curl https://api.yourdomain.com/api/health
```

---

## ?? Files Overview

| File | Purpose | Commit to Git? |
|------|---------|----------------|
| `appsettings.json` | Base settings | ? Yes |
| `appsettings.Production.json` | Production settings (NO connection string) | ? Yes |
| `appsettings.Production.template.json` | Template for production | ? Yes |
| `.env` | Local environment variables | ? NO |
| `.env.example` | Template for .env | ? Yes |
| `setup-connection-windows.ps1` | Windows setup script | ? Yes |
| `setup-connection-linux.sh` | Linux setup script | ? Yes |
| `setup-azure-keyvault.ps1` | Azure Key Vault setup | ? Yes |
| `SECURE-CONNECTION-STRING.md` | Full documentation | ? Yes |

---

## ?? Deployment Checklist

- [ ] Connection string set via environment variable or Key Vault
- [ ] NO connection string in appsettings.Production.json
- [ ] Tested connection via health endpoint
- [ ] .gitignore updated (protects .env and secrets)
- [ ] Service/IIS restarted after setting environment variable

---

## ?? Troubleshooting

**Error: "No connection string found"**
- ? Check environment variable is set correctly
- ? Restart IIS/service after setting variable
- ? Variable name is case-sensitive on Linux

**Still using appsettings.json?**
- ? Verify environment variable: `echo $DIGISTORE_CONNECTION_STRING`
- ? Check for typos in variable name
- ? Clear bin/obj folders and rebuild

**Connection fails**
- ? Test connection string syntax
- ? Check firewall rules
- ? Verify SQL Server allows remote connections

---

## ?? Documentation

- **Full Guide**: `SECURE-CONNECTION-STRING.md`
- **Deployment**: `DEPLOYMENT.md`
- **Quick Start**: `QUICKSTART.md`

---

## ?? Example Connection Strings

**SQL Server:**
```
Server=myserver.database.windows.net;Database=DigiStore;User Id=admin;Password=MyP@ssw0rd;TrustServerCertificate=true;
```

**Windows Authentication:**
```
Server=myserver;Database=DigiStore;Integrated Security=true;
```

**Azure SQL:**
```
Server=tcp:myserver.database.windows.net,1433;Database=DigiStore;User ID=admin@myserver;Password=MyP@ssw0rd;Encrypt=True;
```

---

**Your connection string is now secure! ??**
