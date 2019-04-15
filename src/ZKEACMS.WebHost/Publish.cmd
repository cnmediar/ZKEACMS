


[Unit]
Description=ZKEACMS

[Service]
WorkingDirectory=/home/wwwroot/asianlink/web
ExecStart=/usr/bin/dotnet /home/wwwroot/asianlink/web/ZKEACMS.WebHost.dll
Restart=always
RestartSec=10
SyslogIdentifier=zkeacms
User=root
Environment=ASPNETCORE_ENVIRONMENT=Production 

[Install]
WantedBy=multi-user.target