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

# Define projects with correct folders (System vs Shared)
$projects = @(
    @{ Name = "$editor.Xrm.Plugins";        Path = "$basePath\System\$editor.Xrm.Plugins" },
    @{ Name = "$editor.Xrm.Utilities";      Path = "$basePath\Shared\$editor.Xrm.Utilities" },
    @{ Name = "$editor.Xrm.Service";        Path = "$basePath\Shared\$editor.Xrm.Service" }
)

# Execute steps
CreateSolution -SolutionName $solutionName -BasePath $basePath -ProjectPath $projectPath #create the solution

foreach ($proj in $projects) {
    $csprojPath = "$($proj.Path)\$($proj.Name).csproj"
    CreateProject -ProjectName $proj.Name -ProjectPath $proj.Path -CsprojPath $csprojPath -SolutionFile $solutionFile #create the project
}

CreateOpportunityPlugin -EditorName $editor -ProjectName "$editor.Xrm.Plugins" -ProjectPath "$basePath\System\$editor.Xrm.Plugins" #create OpportunityPlugin.cs
CreateOpportunityService -EditorName $editor -ProjectName "$editor.Xrm.Service" -ProjectPath "$basePath\Shared\$editor.Xrm.Service" #create OpportunityService.cs

Write-Host "SOLUTION $clientApplicationName CREATED FOR $editor WITH SUCCESS (targeting .NET Framework 4.6.2)" -ForegroundColor Green
