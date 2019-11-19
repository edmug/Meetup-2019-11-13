$serviceName = "BlazorServerApp"
$description = "BlazorApp Service Application"
$displayName = "Blazor Server Application"
$startUpType = "Automatic"
$binaryPath = "$PSScriptRoot/Blazor.Server.exe"

Write-Host "Updating Service: [$serviceName]..."
Write-Host "BinaryPath: $binaryPath"

if (Get-Service $serviceName -ErrorAction SilentlyContinue)
{
    Write-Host "Removing existing service: [$serviceName]..."
    # use WMI to remove service because PowerShell does not have CmdLet for this until PS6
    $serviceToRemove = Get-WmiObject -Class Win32_Service -Filter "name='$serviceName'"
    $serviceToRemove.StopService()
    $serviceToRemove.delete()
    Write-Host "Removing existing service: [$serviceName]...done."
}

# Creating Windows Service using all provided parameters
Write-Host "Installing service: $serviceName"
New-Service -name $serviceName -binaryPathName $binaryPath -Description $description -displayName $displayName -startupType $startUpType

Write-Host "Updating Service: [$serviceName]...done."