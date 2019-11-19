$serviceName = "BlazorServerApp"

if (Get-Service $serviceName -ErrorAction SilentlyContinue)
{
    Write-Host "Removing existing service: [$serviceName]..."
    # use WMI to remove service because PowerShell does not have CmdLet for this until PS6
    $serviceToRemove = Get-WmiObject -Class Win32_Service -Filter "name='$serviceName'"
    $serviceToRemove.StopService()
    $serviceToRemove.delete()
    Write-Host "Removing existing service: [$serviceName]...done."
}