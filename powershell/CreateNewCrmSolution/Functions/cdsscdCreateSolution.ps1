function CreateSolution
{
    param (
        [string]$SolutionName,
        [string]$BasePath
    )
    New-Item -ItemType Directory -Path $BasePath | Out-Null
    dotnet new sln -n $SolutionName -o $BasePath | Out-Null

    Write-Host "Solution $SolutionName created at $BasePath" -ForegroundColor Cyan
    Write-Host
}
