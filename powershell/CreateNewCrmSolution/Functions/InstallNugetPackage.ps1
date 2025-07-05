function InstallNugetPackage
{
    param (
        [string]$BasePath,
        [string]$PackageName = "Microsoft.CrmSdk.CoreAssemblies",
        [string]$NugetUrl = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
    )

    $nugetExePath = Join-Path $BasePath "nuget.exe"

    # 1. Download nuget.exe if missing
    if (-not (Test-Path $nugetExePath)) {
        Write-Host "Downloading nuget.exe into $BasePath..." -ForegroundColor Yellow
        Invoke-WebRequest -Uri $NugetUrl -OutFile $nugetExePath -UseBasicParsing
        if (-not (Test-Path $nugetExePath)) {
            Write-Error "nuget.exe could not be downloaded."
            return
        }
    }

    # 2. Download the package in $BasePath\packages
    $packageOutputDir = Join-Path $BasePath "packages"
    $nugetArgs = "install", $PackageName, "-OutputDirectory", $packageOutputDir
    Write-Host "Installing package $PackageName into $packageOutputDir..." -ForegroundColor Yellow
    & $nugetExePath @nugetArgs

    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to install NuGet package $PackageName."
    } else {
        Write-Host "Package $PackageName successfully installed in $packageOutputDir" -ForegroundColor Yellow
        Write-Host
    }
}
