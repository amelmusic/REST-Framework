#runs all powershell scripts for creating PP files and transforming nuget package file

$dir = (Get-Item -Path ".\" -Verbose).FullName

cd ".\A.Core.Services\Core"
$command =  ".\CreatePPFiles.ps1" + " -namespace 'namespace A.Core.Services.Core'"
Invoke-Expression $command
cd $dir


cd ".\A.Core.WebAPI\Core"
$command =  ".\CreatePPFiles.ps1" + " -namespace 'namespace A.Core.WebAPI.Core'"
Invoke-Expression $command
cd $dir

cd ".\A.Core.RESTClient\Core"
$command =  ".\CreatePPFiles.ps1" + " -namespace 'namespace A.Core.RESTClient.Core'"
Invoke-Expression $command
cd $dir

cd ".\A.Core.Scheduler"
$command =  ".\CreatePPFiles.ps1" + " -namespace 'namespace A.Core.Scheduler'"
Invoke-Expression $command
cd $dir