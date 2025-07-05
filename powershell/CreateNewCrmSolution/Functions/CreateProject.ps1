function CreateProject
{
    param (
        [string]$ProjectName,
        [string]$ProjectPath,
        [string]$CsprojPath,
        [string]$SolutionFile
    )
    #Create the folder project
    New-Item -ItemType Directory -Path $ProjectPath | Out-Null

    # Create .csproj file targeting .NET Framework 4.6.2
    $csprojContent =@'
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
    $csprojContent = $csprojContent -replace '__ROOT_NAMESPACE__', $ProjectName
    $csprojContent = $csprojContent -replace '__ASSEMBLY_NAME__', $ProjectName
    Set-Content -Path $CsprojPath -Value $csprojContent
    Write-Host "Project $ProjectName created at $ProjectPath with .csproj" -ForegroundColor Yellow

    #Create the Properties folder with AssemblyInfo.cs
    $propertiesPath = Join-Path $ProjectPath "Properties"
    New-Item -ItemType Directory -Path $propertiesPath | Out-Null

    $assemblyContent =@"
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    // Les informations générales relatives à un assembly dépendent de
    // l'ensemble d'attributs suivant. Changez les valeurs de ces attributs pour modifier les informations
    // associées à un assembly.
    [assembly: AssemblyTitle("$ProjectName")]
    [assembly: AssemblyDescription("")]
    [assembly: AssemblyConfiguration("")]
    [assembly: AssemblyCompany("HP Inc.")]
    [assembly: AssemblyProduct("$ProjectName")]
    [assembly: AssemblyCopyright("Copyright © HP Inc. 2025")]
    [assembly: AssemblyTrademark("")]
    [assembly: AssemblyCulture("")]

    // L'affectation de la valeur false à ComVisible rend les types invisibles dans cet assembly
    // aux composants COM. Si vous devez accéder à un type dans cet assembly à partir de
    // COM, affectez la valeur true à l'attribut ComVisible sur ce type.
    [assembly: ComVisible(false)]

    // Le GUID suivant est pour l'ID de la typelib si ce projet est exposé à COM
    [assembly: Guid("047ec196-2b65-45d2-8cf8-e7c146ed2088")]

    // Les informations de version pour un assembly se composent des quatre valeurs suivantes :
    //
    //      Version principale
    //      Version secondaire
    //      Numéro de build
    //      Révision
    //
    [assembly: AssemblyVersion("1.0.0.0")]
    [assembly: AssemblyFileVersion("1.0.0.0")]
"@
    Set-Content -Path (Join-Path $propertiesPath "AssemblyInfo.cs") -Value $assemblyContent

    Write-Host "Project properties folder created with AssemblyInfo.cs at $propertiesPath" -ForegroundColor Yellow

    # Add the project to the solution
    dotnet sln $SolutionFile add $CsprojPath

    Write-Host "Project added to the solution" -ForegroundColor Yellow
    Write-Host
}
