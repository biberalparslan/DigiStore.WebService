#!/bin/bash
# Deployment Script for Linux

echo "Building DigiStore WebService..."
dotnet clean
dotnet restore
dotnet publish -c Release -o ./publish --self-contained false

echo ""
echo "Build completed successfully!"
echo "Published files are in: ./publish"
echo ""
echo "Next steps:"
echo "1. Copy the publish folder to your server: scp -r ./publish user@server:/var/www/digistore"
echo "2. Update appsettings.Production.json with your configuration"
echo "3. Configure systemd service and Nginx"
echo ""
