function CreateProject
{
    param (
        [string]$ProjectName,
        [string]$ProjectPath
    )
    New-Item -ItemType Directory -Path $ProjectPath | Out-Null

    Write-Host "Project $ProjectName created at $ProjectPath" -ForegroundColor Yellow
    Write-Host
}
