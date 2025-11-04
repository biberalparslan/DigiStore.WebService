# Deployment Scripts

## Windows - Build and Publish
@echo off
echo Building DigiStore WebService...
dotnet clean
dotnet restore
dotnet publish -c Release -o ./publish --self-contained false

echo.
echo Build completed successfully!
echo Published files are in: ./publish
echo.
echo Next steps:
echo 1. Copy the publish folder to your server
echo 2. Update appsettings.Production.json with your configuration
echo 3. Configure IIS or your hosting environment
echo.
pause
