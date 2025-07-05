#Import all functions
Get-ChildItem "$PSScriptRoot\Functions\*.ps1" | ForEach-Object { . $_.FullName }

# Reset variables
if (Test-Path Variable:clientApplicationName) { Remove-Variable clientApplicationName -Force -ErrorAction SilentlyContinue}
if (Test-Path Variable:editor) { Remove-Variable editor -Force -ErrorAction SilentlyContinue}

# Ask for user inputs
$clientApplicationName = Read-Host "Enter the client application name in capital letters"
$editor = Read-Host "Enter the client editor name"

# Define paths and names
$basePath = "C:\Temp\source\repos\$clientApplicationName\$editor.Xrm"
$solutionName = "$editor.Xrm"
$projectName = "$editor.Xrm.Plugins"
$projectPath = "$basePath\$projectName"
$csprojPath = "$projectPath\$projectName.csproj"
$solutionFile = "$basePath\$editor.Xrm.sln"

# Execute steps
CreateSolution -SolutionName $solutionName -BasePath $basePath #create the solution
CreateProject -ProjectName $projectName -ProjectPath $projectPath -CsprojPath $csprojPath -SolutionFile $solutionFile #create the project
CreateClass -ProjectName $projectName -ProjectPath $projectPath #create a basic Class1.cs file

Write-Host "SOLUTION $clientApplicationName CREATED FOR $editor WITH SUCCESS (targeting .NET Framework 4.6.2)" -ForegroundColor Green
