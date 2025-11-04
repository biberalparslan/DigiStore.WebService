# ================================
# Azure Key Vault Setup Script
# ================================

Write-Host "DigiStore API - Azure Key Vault Setup" -ForegroundColor Cyan
Write-Host "======================================" -ForegroundColor Cyan
Write-Host ""

# Check if Azure CLI is installed
try {
    $azVersion = az version 2>$null
    if (-not $azVersion) { throw }
} catch {
    Write-Host "? Azure CLI is not installed" -ForegroundColor Red
    Write-Host "Install from: https://docs.microsoft.com/cli/azure/install-azure-cli"
    exit 1
}

# Login to Azure
Write-Host "Logging in to Azure..." -ForegroundColor Yellow
az login

# Get parameters
$resourceGroup = Read-Host "`nEnter Resource Group name"
$keyVaultName = Read-Host "Enter Key Vault name (must be globally unique)"
$location = Read-Host "Enter Azure region [eastus]"
$location = if ($location) { $location } else { "eastus" }

# Get connection string
$connectionString = Read-Host "`nEnter your database connection string" -AsSecureString
$connectionStringPlain = [Runtime.InteropServices.Marshal]::PtrToStringAuto(
    [Runtime.InteropServices.Marshal]::SecureStringToBSTR($connectionString))

# Create Resource Group if it doesn't exist
Write-Host "`nChecking resource group..." -ForegroundColor Yellow
$rgExists = az group exists --name $resourceGroup
if ($rgExists -eq "false") {
    Write-Host "Creating resource group: $resourceGroup" -ForegroundColor Yellow
    az group create --name $resourceGroup --location $location
    Write-Host "? Resource group created" -ForegroundColor Green
}

# Create Key Vault
Write-Host "`nCreating Key Vault: $keyVaultName" -ForegroundColor Yellow
try {
    az keyvault create `
        --name $keyVaultName `
        --resource-group $resourceGroup `
        --location $location `
        --enable-rbac-authorization false
    
    Write-Host "? Key Vault created" -ForegroundColor Green
} catch {
    Write-Host "??  Key Vault might already exist or name is taken" -ForegroundColor Yellow
}

# Add connection string as secret
Write-Host "`nAdding connection string to Key Vault..." -ForegroundColor Yellow
az keyvault secret set `
    --vault-name $keyVaultName `
    --name "ConnectionStrings--DefaultConnection" `
    --value $connectionStringPlain

Write-Host "? Secret added to Key Vault" -ForegroundColor Green

# Get current user
$currentUser = az account show --query user.name -o tsv
Write-Host "`nCurrent user: $currentUser" -ForegroundColor Cyan

# Ask about App Service
$hasAppService = Read-Host "`nDo you have an Azure App Service? (y/n)"
if ($hasAppService -eq "y") {
    $appServiceName = Read-Host "Enter App Service name"
    
    # Enable Managed Identity
    Write-Host "`nEnabling Managed Identity for App Service..." -ForegroundColor Yellow
    az webapp identity assign `
        --name $appServiceName `
        --resource-group $resourceGroup
    
    # Get principal ID
    $principalId = az webapp identity show `
        --name $appServiceName `
        --resource-group $resourceGroup `
        --query principalId -o tsv
    
    # Grant access to Key Vault
    Write-Host "Granting Key Vault access to App Service..." -ForegroundColor Yellow
    az keyvault set-policy `
        --name $keyVaultName `
        --object-id $principalId `
        --secret-permissions get list
    
    Write-Host "? App Service configured" -ForegroundColor Green
    
    # Add KeyVaultName to App Service configuration
    Write-Host "Adding KeyVaultName to App Service configuration..." -ForegroundColor Yellow
    az webapp config appsettings set `
        --name $appServiceName `
        --resource-group $resourceGroup `
        --settings KeyVaultName=$keyVaultName
    
    Write-Host "? App Service configuration updated" -ForegroundColor Green
}

# Summary
Write-Host ""
Write-Host "======================================" -ForegroundColor Cyan
Write-Host "? Azure Key Vault Setup Complete!" -ForegroundColor Green
Write-Host "======================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Key Vault Details:" -ForegroundColor Yellow
Write-Host "  Name: $keyVaultName"
Write-Host "  Resource Group: $resourceGroup"
Write-Host "  URL: https://$keyVaultName.vault.azure.net/"
Write-Host ""
Write-Host "Secret Name:" -ForegroundColor Yellow
Write-Host "  ConnectionStrings--DefaultConnection"
Write-Host ""
Write-Host "Next Steps:" -ForegroundColor Yellow
Write-Host "1. Add to your appsettings.json or App Service configuration:"
Write-Host "   {" -ForegroundColor Gray
Write-Host "     `"KeyVaultName`": `"$keyVaultName`"" -ForegroundColor Gray
Write-Host "   }" -ForegroundColor Gray
Write-Host ""
Write-Host "2. For local development, login with Azure CLI:"
Write-Host "   az login" -ForegroundColor Gray
Write-Host ""
Write-Host "3. Test your connection:"
Write-Host "   https://yourapp.azurewebsites.net/api/health" -ForegroundColor Gray
Write-Host ""
Write-Host "View secret in Azure Portal:" -ForegroundColor Yellow
Write-Host "https://portal.azure.com/#@/resource/subscriptions/YOUR_SUBSCRIPTION/resourceGroups/$resourceGroup/providers/Microsoft.KeyVault/vaults/$keyVaultName/secrets" -ForegroundColor Cyan
