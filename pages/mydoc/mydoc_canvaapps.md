---
title: Canva Apps
keywords: canva apps, power fx
summary: "Canva Apps allows users to quickly build custom mobile or web apps using a drag-and-drop interface. They connect to many data sources and enable real-time data interaction, helping organizations simplify processes and boost productivity"
sidebar: mydoc_sidebar
toc: false
permalink: mydoc_canvaapps.html
folder: mydoc
---

<ul id="profileTabs" class="nav nav-tabs">
    <li class="active"><a class="noCrossRef" href="#general" data-toggle="tab">General</a></li>
    <li><a class="noCrossRef" href="#powerfx" data-toggle="tab">C#</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="general" markdown="1">
## 📚 Documentation
* <a href="https://learn.microsoft.com/en-us/power-apps/maker/canvas-apps/sharepoint-form-integration" target="_blank" rel="noopener noreferrer">SharePoint forms integration</a>
* <a href="https://learn.microsoft.com/en-us/power-apps/guidance/coding-guidelines/code-readability" target="_blank" rel="noopener noreferrer">Naming conventions</a>
* <a href="https://learn.microsoft.com/en-us/power-apps/guidance/coding-guidelines/code-readability" target="_blank" rel="noopener noreferrer">Code optimization</a>
</div>

<div role="tabpanel" class="tab-pane" id="powerfx" markdown="1">
### Create a new plugin assembly from scratch
1. <a href="https://github.com/gedeontofidji/PowerShell/tree/main/CreateDynamicsPlugin" target="_blank" rel="noopener noreferrer">Download the repository locally</a>. `PowerShell may not run the script due to execution policy. In that case, create each script file manually by copying the content.`
2. In Main.ps1 script, configure the `basePath` variable to the location where you want to create the solution
3. In PowerShell, navigate to the folder where Main.ps1 is located, and run it
4. Edit Cor.Xrm.Plugins.csproj to add `<PropertyGroup><PostBuildEvent>.....</PostBuildEvent></PropertyGroup>` with the right editorName
5. Add :
* <a href="https://github.com/gedeontofidji/ClassLibrary_NetFramework/tree/main/CORE/Cor.Xrm/System/Cor.Xrm.Plugins/AppCode" target="_blank" rel="noopener noreferrer">AppCode folder</a> in Plugins project
* <a href="https://github.com/gedeontofidji/ClassLibrary_NetFramework/tree/main/CORE/Cor.Xrm/Shared/Cor.Xrm.Utilities/Extensions" target="_blank" rel="noopener noreferrer">Extensions folder</a>, <a href="https://github.com/gedeontofidji/ClassLibrary_NetFramework/blob/main/CORE/Cor.Xrm/Shared/Cor.Xrm.Utilities/SerializerHelper.cs" target="_blank" rel="noopener noreferrer">SerializerHelper.cs</a> and <a href="https://github.com/gedeontofidji/ClassLibrary_NetFramework/blob/main/CORE/Cor.Xrm/Shared/Cor.Xrm.Utilities/SharingHelper.cs" target="_blank" rel="noopener noreferrer">SharingHelper.cs</a> in Utilities project
* <a href="https://github.com/gedeontofidji/ClassLibrary_NetFramework/tree/main/CORE/Cor.Xrm/Shared/Cor.Xrm.Service/Base" target="_blank" rel="noopener noreferrer">Base folder</a> in Service project
* <a href="https://github.com/gedeontofidji/ClassLibrary_NetFramework/blob/main/CORE/Cor.Xrm/DLaB.EarlyBoundGenerator.DefaultSettings.xml" target="_blank" rel="noopener noreferrer">EarlyBoundGenerator.DefaultSettings.xml</a> in Cor.Xrm and edit it with the editorName
6. In VisualStudio: 
* Replace `Cor` everywhere by the editorName
* Add to Plugins project the reference to Service and Utilities projects
* Install `ILMerge` NuGet package in Plugins project
* Install `Microsoft.CrmSdk.CoreAssemblies` NuGet package in Service, Utilities and Plugins projects
* Install `MscrmTools.FluentQueryExpressions` NuGet package in Service and Plugins projects
* Right click on the solution > Add new project > Search for `C# shared project` and create `Cor.Xrm.EntityWrappers` in the Shared folder
* Right click on the plugin projet > Properties > Signing and sign the assembly with `Cor.Xrm.snk file` to be able to register it in XrmToolBox.
7. Use EarlyBoundGenerator to generate classes. `Copy the settings from another environment`
8. Update `AssemblyInfo.cs file` in Plugin project

### Coding process
`The logic must be implemented in the service (e.g: CodeNasService) and not in the class (e.g: CodeNas).`  
Pull the code stored on Git > Add your code and save it > Build the solution to generates the DLL in the solution folder > Open Plugin Registration in XrmToolBox to update the plugin with the generated DLL > Test > Enter the commit message, then push the changes to Git.
{% include image.html file="visualstudio_pull.png" max-width="30%" %}
{% include image.html file="visualstudio_push.png" max-width="30%" %}

### ⌘ Keyboard shortcuts (Microsoft Visual Studio)  
* Ctrl+Shift+B: Build the solution
* Ctrl+Space: Autocompletion

### Enable/Disable a service method
`IsFeatureEnabled(nameof(OpportunityService.service))` is defined in XrmToolBox step
{% include image.html file="powerapps_cs.png" max-width="50%" %}

```
if (intrant.cve_CreatedFromOutlook == true)
{
    var currentBpf = new Query<cve_bpfintrantaval>()
        .Select(true)
        .WhereEqual(l => l.bpf_cve_intranetavalid, intrant.Id)
        .GetFirstOrDefault(UserService);

    currentBpf.ActiveStageId.Id = new Guid("38648cd2-fc14-4eff-a1d5-51182e4a0c42"); // Id de la phase 2
    currentBpf.TraversedPath = "002e7ec5-16fd-4f39-b9a0-fc1ea2524c6e,38648cd2-fc14-4eff-a1d5-51182e4a0c42"; // Transition de la phase 1 à 2
    UserService.Update(currentBpf);
}
TraceMethodEnd();
```
</div>
</div>
{% include links.html %}
