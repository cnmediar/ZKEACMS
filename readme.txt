/usr/bin/dotnet /home/wwwroot/asianlink/web/ZKEACMS.WebHost.dll

sudo vi /etc/systemd/system/zkeacms.service

chown www.www -R ./


sudo vi /etc/systemd/system/zkeacms.service


systemctl restart   zkeacms.service
