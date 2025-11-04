# Secure Connection String Configuration Guide

## ?? Security Warning
**NEVER store production connection strings in appsettings.json or any file in your project!**

This guide shows you how to securely store and use connection strings in production.

---

## Methods to Securely Store Connection Strings

### ? Method 1: Environment Variables (Recommended for Most Scenarios)

Environment variables are stored at the OS level and are not accessible through file system.

#### Windows Server (IIS)

**Option A: IIS Application Settings**
1. Open IIS Manager
2. Select your site ? Configuration Editor
3. Section: `system.webServer/aspNetCore`
4. Click on `environmentVariables` ? "..." button
5. Add new environment variable:
   - Name: `DIGISTORE_CONNECTION_STRING`
   - Value: Your actual connection string
6. Click OK and restart the Application Pool

**Option B: Windows Environment Variables**
1. Open System Properties ? Advanced ? Environment Variables
2. Add System Variable:
   - Name: `DIGISTORE_CONNECTION_STRING`
   - Value: `Server=myserver;Database=mydb;User Id=myuser;Password=mypassword;`
3. Restart IIS or Application Pool

**PowerShell Script:**
```powershell
# Set environment variable (requires admin)
[System.Environment]::SetEnvironmentVariable(
    'DIGISTORE_CONNECTION_STRING',
    'Server=myserver;Database=mydb;User Id=myuser;Password=mypassword;TrustServerCertificate=true',
    [System.EnvironmentVariableTarget]::Machine
)

# Restart IIS
iisreset
```

#### Linux (systemd service)

Edit your systemd service file `/etc/systemd/system/digistore.service`:

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
Environment=DIGISTORE_CONNECTION_STRING=Server=myserver;Database=mydb;User Id=myuser;Password=mypassword;

[Install]
WantedBy=multi-user.target
```

Then reload and restart:
```bash
sudo systemctl daemon-reload
sudo systemctl restart digistore.service
```

#### Docker / Docker Compose

**docker-compose.yml:**
```yaml
version: '3.8'

services:
  digistore-api:
    image: digistore-api
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - DIGISTORE_CONNECTION_STRING=Server=myserver;Database=mydb;User Id=myuser;Password=mypassword;
    # Or use .env file (keep .env out of git)
    env_file:
      - .env
```

**Docker run command:**
```bash
docker run -d \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e DIGISTORE_CONNECTION_STRING="Server=myserver;Database=mydb;User Id=myuser;Password=mypassword;" \
  -p 80:80 \
  digistore-api
```

**Using .env file (recommended):**
Create `.env` file (add to .gitignore):
```
DIGISTORE_CONNECTION_STRING=Server=myserver;Database=mydb;User Id=myuser;Password=mypassword;
```

---

### ? Method 2: Azure Key Vault (Best for Azure/Cloud)

Azure Key Vault is the most secure option for cloud deployments.

#### Setup Steps:

**1. Create Key Vault and Add Secret:**
```bash
# Login to Azure
az login

# Create Key Vault
az keyvault create --name digistore-keyvault --resource-group MyResourceGroup --location eastus

# Add connection string as secret
az keyvault secret set \
  --vault-name digistore-keyvault \
  --name DigiStoreConnectionString \
  --value "Server=myserver;Database=mydb;User Id=myuser;Password=mypassword;"
```

**2. Install Required Package:**
```bash
cd DigiStore.WebService
dotnet add package Azure.Identity
dotnet add package Azure.Extensions.AspNetCore.Configuration.Secrets
```

**3. Update Program.cs:**

Your application will need this modification (I'll do it next).

**4. Grant Access:**
- For App Service: Enable Managed Identity
- For local development: Use Azure CLI login

```bash
# Grant App Service access to Key Vault
az keyvault set-policy \
  --name digistore-keyvault \
  --object-id <your-app-service-managed-identity-id> \
  --secret-permissions get list
```

**5. Configure App Service:**
In Azure Portal ? App Service ? Configuration, add:
- Name: `KeyVaultName`
- Value: `digistore-keyvault`

---

### ? Method 3: User Secrets (Development Only)

For development, use .NET User Secrets - never committed to source control.

```bash
# Navigate to project directory
cd DigiStore.WebService

# Initialize user secrets
dotnet user-secrets init

# Set connection string
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=DigiStoreDb;Integrated Security=true;"

# List all secrets
dotnet user-secrets list

# Remove a secret
dotnet user-secrets remove "ConnectionStrings:DefaultConnection"

# Clear all secrets
dotnet user-secrets clear
```

**Location:** User secrets are stored at:
- Windows: `%APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json`
- Linux/Mac: `~/.microsoft/usersecrets/<user_secrets_id>/secrets.json`

---

### ? Method 4: AWS Secrets Manager (for AWS)

If deploying to AWS, use Secrets Manager.

**1. Install Package:**
```bash
dotnet add package Amazon.Extensions.Configuration.SystemsManager
```

**2. Create Secret in AWS:**
```bash
aws secretsmanager create-secret \
  --name DigiStoreConnectionString \
  --secret-string "Server=myserver;Database=mydb;User Id=myuser;Password=mypassword;"
```

**3. Configure in Program.cs** (see implementation below)

---

### ? Method 5: HashiCorp Vault (Enterprise)

For enterprise environments using HashiCorp Vault.

**1. Install Package:**
```bash
dotnet add package VaultSharp
```

**2. Configuration** will be handled in code (see implementation)

---

## How It Works in Your Application

Your application now checks for connection strings in this priority order:

1. **Environment Variable** `DIGISTORE_CONNECTION_STRING` (highest priority)
2. **Configuration Provider** (Key Vault, AWS Secrets, etc.)
3. **User Secrets** (development)
4. **appsettings.json** (fallback - not recommended)

---

## Quick Reference

### Production Deployment Checklist

- [ ] Remove connection string from all appsettings files
- [ ] Set connection string using one of the secure methods above
- [ ] Verify `.gitignore` excludes sensitive files
- [ ] Test connection before going live
- [ ] Document which method you used for your team

### Testing Connection String

**Method 1: Check application startup logs**
The application will print which method is being used:
```
Using connection string from environment variable
```

**Method 2: Use health endpoint**
Access: `https://api.yourdomain.com/api/health`

---

## Security Best Practices

1. ? **Use environment variables or Key Vault** for production
2. ? **Use User Secrets** for local development
3. ? **Never commit** sensitive data to Git
4. ? **Rotate credentials** regularly
5. ? **Use minimal permissions** for database accounts
6. ? **Enable TLS/SSL** for database connections
7. ? **Audit access** to secrets regularly
8. ? **Use different credentials** for dev/staging/production

---

## Troubleshooting

### Error: "No connection string found"
- Verify environment variable is set correctly
- Check variable name: `DIGISTORE_CONNECTION_STRING` (case-sensitive on Linux)
- Restart service/application pool after setting environment variables

### Connection string not updating
- Clear .NET cache: Delete bin/ and obj/ folders
- Restart IIS Application Pool (Windows)
- Restart systemd service (Linux)
- Rebuild Docker container

### Still using appsettings.json
- Check if environment variable is actually set: `echo $env:DIGISTORE_CONNECTION_STRING` (Windows) or `echo $DIGISTORE_CONNECTION_STRING` (Linux)
- Verify no typos in variable name

---

## Examples

### Complete Connection String Examples

**SQL Server with SQL Authentication:**
```
Server=myserver.database.windows.net;Database=DigiStoreDb;User Id=myuser;Password=myP@ssw0rd;TrustServerCertificate=true;
```

**SQL Server with Windows Authentication:**
```
Server=myserver;Database=DigiStoreDb;Integrated Security=true;
```

**Azure SQL Database:**
```
Server=tcp:myserver.database.windows.net,1433;Database=DigiStoreDb;User ID=myuser@myserver;Password=myP@ssw0rd;Encrypt=True;TrustServerCertificate=False;
```

**SQL Server with Encryption:**
```
Server=myserver;Database=DigiStoreDb;User Id=myuser;Password=myP@ssw0rd;Encrypt=True;TrustServerCertificate=False;
```

---

## Need Help?

- Azure Key Vault: https://docs.microsoft.com/azure/key-vault/
- .NET User Secrets: https://docs.microsoft.com/aspnet/core/security/app-secrets
- Environment Variables: https://docs.microsoft.com/aspnet/core/fundamentals/configuration/
