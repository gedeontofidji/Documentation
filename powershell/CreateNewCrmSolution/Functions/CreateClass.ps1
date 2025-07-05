function CreateClass
{
    param (
        [string]$ProjectName,
        [string]$ProjectPath
    )

    $classFile = Join-Path $ProjectPath "Class1.cs"
    $classContent = @"
namespace $ProjectName
{
    public class Class1
    {
    }
}
"@
    Set-Content -Path $classFile -Value $classContent

    Write-Host "Class1.cs created at $ProjectPath" -ForegroundColor Cyan
    Write-Host
}
