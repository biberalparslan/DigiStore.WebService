# ================================
# Windows IIS - Set Connection String
# ================================
# Run this script as Administrator

$connectionString = Read-Host "Enter your database connection string"

# Method 1: Set as System Environment Variable (recommended)
Write-Host "`nSetting system environment variable..." -ForegroundColor Green
[System.Environment]::SetEnvironmentVariable(
    'DIGISTORE_CONNECTION_STRING',
    $connectionString,
    [System.EnvironmentVariableTarget]::Machine
)

Write-Host "? Environment variable set successfully!" -ForegroundColor Green

# Method 2: Update web.config (alternative)
$webConfigPath = Read-Host "`nEnter path to web.config (or press Enter to skip)"
if ($webConfigPath -and (Test-Path $webConfigPath)) {
    [xml]$webConfig = Get-Content $webConfigPath
    
    $envVars = $webConfig.configuration.location.'system.webServer'.aspNetCore.environmentVariables
    if (-not $envVars) {
        $envVarsNode = $webConfig.CreateElement("environmentVariables")
        $webConfig.configuration.location.'system.webServer'.aspNetCore.AppendChild($envVarsNode) | Out-Null
        $envVars = $webConfig.configuration.location.'system.webServer'.aspNetCore.environmentVariables
    }
    
    # Remove existing if present
    $existing = $envVars.environmentVariable | Where-Object { $_.name -eq "DIGISTORE_CONNECTION_STRING" }
    if ($existing) {
        $envVars.RemoveChild($existing) | Out-Null
    }
    
    # Add new
    $envVar = $webConfig.CreateElement("environmentVariable")
    $envVar.SetAttribute("name", "DIGISTORE_CONNECTION_STRING")
    $envVar.SetAttribute("value", $connectionString)
    $envVars.AppendChild($envVar) | Out-Null
    
    $webConfig.Save($webConfigPath)
    Write-Host "? web.config updated successfully!" -ForegroundColor Green
}

# Restart IIS
Write-Host "`nRestarting IIS..." -ForegroundColor Yellow
iisreset /restart

Write-Host "`n? Configuration complete!" -ForegroundColor Green
Write-Host "`nYour connection string is now stored securely as an environment variable."
Write-Host "The connection string is NOT stored in any JSON file."

# Test
Write-Host "`nTesting environment variable..."
$test = [System.Environment]::GetEnvironmentVariable('DIGISTORE_CONNECTION_STRING', [System.EnvironmentVariableTarget]::Machine)
if ($test) {
    Write-Host "? Connection string is set (length: $($test.Length) characters)" -ForegroundColor Green
} else {
    Write-Host "? Warning: Could not verify environment variable" -ForegroundColor Red
}
