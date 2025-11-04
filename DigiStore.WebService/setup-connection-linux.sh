#!/bin/bash
# ================================
# Linux - Set Connection String
# ================================

echo "DigiStore API - Secure Connection String Setup"
echo "=============================================="
echo ""

# Get connection string from user
read -sp "Enter your database connection string: " CONNECTION_STRING
echo ""

if [ -z "$CONNECTION_STRING" ]; then
    echo "? Error: Connection string cannot be empty"
    exit 1
fi

# Get service name
read -p "Enter systemd service name [digistore]: " SERVICE_NAME
SERVICE_NAME=${SERVICE_NAME:-digistore}
SERVICE_FILE="/etc/systemd/system/${SERVICE_NAME}.service"

# Check if service file exists
if [ ! -f "$SERVICE_FILE" ]; then
    echo "??  Warning: Service file not found at $SERVICE_FILE"
    read -p "Do you want to create it? (y/n): " CREATE_SERVICE
    
    if [ "$CREATE_SERVICE" = "y" ]; then
        read -p "Enter working directory [/var/www/digistore]: " WORK_DIR
        WORK_DIR=${WORK_DIR:-/var/www/digistore}
        
        echo "Creating service file..."
        sudo tee "$SERVICE_FILE" > /dev/null <<EOF
[Unit]
Description=DigiStore Web API
After=network.target

[Service]
WorkingDirectory=$WORK_DIR
ExecStart=/usr/bin/dotnet $WORK_DIR/DigiStore.WebService.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=digistore-api
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false
Environment=DIGISTORE_CONNECTION_STRING=$CONNECTION_STRING

[Install]
WantedBy=multi-user.target
EOF
        echo "? Service file created"
    else
        echo "Exiting..."
        exit 1
    fi
else
    # Update existing service file
    echo "Updating existing service file..."
    
    # Check if environment variable already exists
    if grep -q "DIGISTORE_CONNECTION_STRING" "$SERVICE_FILE"; then
        # Update existing
        sudo sed -i "s|Environment=DIGISTORE_CONNECTION_STRING=.*|Environment=DIGISTORE_CONNECTION_STRING=$CONNECTION_STRING|" "$SERVICE_FILE"
    else
        # Add new
        sudo sed -i "/Environment=ASPNETCORE_ENVIRONMENT=Production/a Environment=DIGISTORE_CONNECTION_STRING=$CONNECTION_STRING" "$SERVICE_FILE"
    fi
    echo "? Service file updated"
fi

# Reload systemd
echo "Reloading systemd daemon..."
sudo systemctl daemon-reload

# Enable service
echo "Enabling service..."
sudo systemctl enable "$SERVICE_NAME.service"

# Restart service
echo "Restarting service..."
sudo systemctl restart "$SERVICE_NAME.service"

# Check status
sleep 2
echo ""
echo "Service status:"
sudo systemctl status "$SERVICE_NAME.service" --no-pager -l

echo ""
echo "=============================================="
echo "? Configuration complete!"
echo ""
echo "Your connection string is stored securely in:"
echo "  $SERVICE_FILE"
echo ""
echo "The connection string is NOT in any JSON file."
echo ""
echo "Useful commands:"
echo "  View logs: sudo journalctl -u $SERVICE_NAME.service -f"
echo "  Restart:   sudo systemctl restart $SERVICE_NAME.service"
echo "  Status:    sudo systemctl status $SERVICE_NAME.service"
echo "=============================================="
