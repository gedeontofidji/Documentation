#Import all functions at the root folder
Get-ChildItem "$PSScriptRoot\Functions\*.ps1" | ForEach-Object { . $_.FullName }

# Reset variables
if (Test-Path Variable:clientApplicationName) { Remove-Variable clientApplicationName -Force -ErrorAction SilentlyContinue}
if (Test-Path Variable:editor) { Remove-Variable editor -Force -ErrorAction SilentlyContinue}

# Ask for user inputs
$clientApplicationName = Read-Host "Enter the client application name in capital letters"
$editor = Read-Host "Enter the client editor name"

# Define paths and names
$basePath = "C:\Temp\source\repos\$clientApplicationName\$editor.Xrm"
$solutionFile = "$basePath\$editor.Xrm.sln"
$projectName = "$editor.Xrm.Plugins"
$projectPath = "$basePath\$projectName"
$csprojPath = "$projectPath\$projectName.csproj"

# Create base directory
New-Item -ItemType Directory -Path $basePath | Out-Null

# Create solution file
dotnet new sln -n "$editor.Xrm" -o $basePath

# Create project directory
New-Item -ItemType Directory -Path $projectPath | Out-Null

# Create .csproj file targeting .NET Framework 4.6.2
$csprojContent = @'
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>047ec196-2b65-45d2-8cf8-e7c146ed2088</ProjectGuid>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>__ROOT_NAMESPACE__</RootNamespace>
    <AssemblyName>__ASSEMBLY_NAME__</AssemblyName>
    <TargetFrameworkVersion>v4.6.2</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <Deterministic>true</Deterministic>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System"/>
    <Reference Include="System.Core"/>
    <Reference Include="System.Xml.Linq"/>
    <Reference Include="System.Data.DataSetExtensions"/>
    <Reference Include="Microsoft.CSharp"/>
    <Reference Include="System.Data"/>
    <Reference Include="System.Net.Http"/>
    <Reference Include="System.Xml"/>
  </ItemGroup>
  <ItemGroup>
    <Compile Include="Class1.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
'@
$csprojContent = $csprojContent -replace '__ROOT_NAMESPACE__', $projectName
$csprojContent = $csprojContent -replace '__ASSEMBLY_NAME__', $projectName
# Writting the .csproj file
Set-Content -Path $csprojPath -Value $csprojContent

# Create Properties folder and AssemblyInfo.cs
CreateProjectProperties -ProjectName $projectName -ProjectPath $projectPath

# Create a basic Class1.cs file
$classFile = "$projectPath\Class1.cs"
$classContent = @"
namespace $projectName
{
    public class Class1
    {
    }
}
"@
Set-Content -Path $classFile -Value $classContent

# Add the project to the solution if not already added
$slnProjects = dotnet sln $solutionFile list
dotnet sln $solutionFile add $csprojPath

Write-Output "Plugin project created for $clientApplicationName at $basePath (targeting .NET Framework 4.6.2)"
