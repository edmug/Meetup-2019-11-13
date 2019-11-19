sc stop "BlazorServerApp"
sc delete "BlazorServerApp"
sc create "BlazorServerApp" binPath="%~dp0/Blazor.Server.exe"
pause
