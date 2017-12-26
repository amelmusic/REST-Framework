param([String]$namespace = @("namespace A.Core.Services.Core")) #Must be the first statement in your script

$dir = (Get-Item -Path ".\" -Verbose).FullName
$destination = $dir + "\"

$templateFile = (Get-ChildItem -Path $dir -Filter *.tt | Select-Object -First 1)

"NAMESPACE: " + $namespace
"TEMPLATE:" + $templateFile
$excluded = $templateFile.Name.Replace(".tt", ".cs")


$files = Get-ChildItem -Path $dir -Recurse -Exclude $excluded | ?{$_.Extension -eq ".cs"}

$files | % {
    $copyto = $destination + $_.Name.Replace('.cs','.cs.pp')
    Copy-Item $_.FullName $copyto -force
    $currentFileContent = Get-Content $copyto
    $currentFileContent = $currentFileContent -replace $namespace, 'namespace $rootnamespace$.Core //DD'
    Set-Content -Path $copyto -Value $currentFileContent
    "FULL: " + $_.FullName
    "TO: " + $copyto
}