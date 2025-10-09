---
title: Power Apps
keywords: power apps, outlook, power fx, js, javascript, c#, csharp
summary: "Power Apps is a Microsoft tool that allows users to build custom applications, simplify business processes, and enhance productivity across an organization. It connects to various data sources, enabling real-time data interaction and app development without extensive coding"
sidebar: mydoc_sidebar
toc: false
permalink: mydoc_powerapps.html
folder: mydoc
---

<ul id="profileTabs" class="nav nav-tabs">
    <li class="active"><a class="noCrossRef" href="#general" data-toggle="tab">General</a></li>
    <li><a class="noCrossRef" href="#csharp" data-toggle="tab">C#</a></li>
    <li><a class="noCrossRef" href="#cli" data-toggle="tab">CLI</a></li>
    <li><a class="noCrossRef" href="#dynamics-app-outlook" data-toggle="tab">Outlook</a></li>
    <li><a class="noCrossRef" href="#javascript" data-toggle="tab">JavaScript</a></li>
    <li><a class="noCrossRef" href="#levelup" data-toggle="tab">Level up for PowerApps</a></li>
    <li><a class="noCrossRef" href="#patch" data-toggle="tab">Patch</a></li>
    <li><a class="noCrossRef" href="#power-bi" data-toggle="tab">Power BI</a></li>
    <li><a class="noCrossRef" href="#power-fx" data-toggle="tab">Power Fx</a></li>
    <li><a class="noCrossRef" href="#pfc" data-toggle="tab">PFC</a></li>
    <li><a class="noCrossRef" href="#sharepoint" data-toggle="tab">SharePoint</a></li>
    <li><a class="noCrossRef" href="#copilot" data-toggle="tab">Copilot</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="general" markdown="1">
## 📚 Documentation
* <a href="https://learn.microsoft.com/en-us/power-platform/alm/custom-host-pipelines" target="_blank" rel="noopener noreferrer">Create a pipeline to deploy solutions</a>
* <a href="https://learn.microsoft.com/en-us/power-apps/maker/data-platform/data-platform-restricted-entities" target="_blank" rel="noopener noreferrer">Restricted tables requiring Dynamics 365 licenses</a>
* <a href="https://mattruma.com/adventures-with-dataverse-virtual-tables-and-look-up-columns/?utm_source=substack&utm_medium=email" target="_blank" rel="noopener noreferrer">Virtual tables and lookup columns</a>
* <a href="https://orby.com.au/hide-new-button-on-lookup-controls-in-model-driven-apps-2/" target="_blank" rel="noopener noreferrer">Hide new button on lookup controls</a>
* <a href="https://learn.microsoft.com/en-us/power-apps/maker/plan-designer/plan-designer" target="_blank" rel="noopener noreferrer">Overview of Plan Designer</a>  
It helps to create a plan for your existing solution or a new one using copilot. It generates a detailed document that describes your solution. The plan covers the business problem, user requirements like user roles and stories, the data model, and technologies like apps. This feature saves time when you're trying to understand a solution's content and helps makers improve an existing solution.

## General
#### ⚙️ How to make a view editable
In the environment:
1. Go to Settings > Customize > Customize the system
2. Select the entity you want to enable the editable grid feature
3. Select the Controls tab > Add Control
4. Choose Editable Grid
5. Make sure the radio button under Web is selected then publish your customizations
{% include image.html file="editableview1.png" max-width="50%" %}
{% include image.html file="editableview2.png" max-width="800%" %}
</div>

<div role="tabpanel" class="tab-pane" id="csharp" markdown="1">
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

<div role="tabpanel" class="tab-pane" id="cli" markdown="1">
## Command Line Interface  
Microsoft Power Platform CLI allows you to perform various operations within Microsoft Power Platform (<a href="https://learn.microsoft.com/fr-fr/power-platform/developer/cli/introduction?tabs=windows#install-power-platform-cli-for-windows" target="_blank" rel="noopener noreferrer">How to intstall PAC</a>)
</div>

<div role="tabpanel" class="tab-pane" id="dynamics-app-outlook" markdown="1">
### 📚 Documentation
* <a href="https://joegill.com/power-apps-outlook-deep-linking/?utm_source=substack&utm_medium=email" target="_blank" rel="noopener noreferrer">Outlook deep linking</a>

### Dynamics 365 App for Outlook
Plugin that connects Outlook with Power Apps, allowing users to track emails or appointments, access or create records directly from Outlook.
* To enable integration of a table with Outlook, make sure the table is included in the 'Dynamics 365 App for Outlook' application.
* To install the plugin on a email user, go in the `application > Advanced setting > Email configuration > Mailboxes` and follow the steps :
{% include image.html file="powerapps_outlookconfiguration.png" max-width="50%" %}  

### ❗ Common issues
* When you connect the plugin to a new environment, it may take some time before it appears in the user's mailbox. It can take up to 24 hours for the settings to take effect
</div>

<div role="tabpanel" class="tab-pane" id="levelup" markdown="1">
Level up for Dynamics 365/Power Apps is a browser extension that helps you to perform advanced actions.
</div>
  
<div role="tabpanel" class="tab-pane" id="javascript" markdown="1">
### 📚 Documentation
* <a href="https://learn.microsoft.com/en-us/power-apps/developer/model-driven-apps/clientapi/reference" target="_blank" rel="noopener noreferrer">JavaScript API</a>
* <a href="https://github.com/gedeontofidji/JavaScript/blob/main/Shared/Form.js" target="_blank" rel="noopener noreferrer">Form.js functions</a>

### 🐞 Debugging
* Press `Fn+f12` to open the debug console
* To help you debug, you can either display an alert popup, write a message in the debug console or stop the code at a specific moment:
```
alert("Field update with succes with the value :" + nom);
console.log("Field update with succes with the value :" + nom );
debugger;
```
</div>

<div role="tabpanel" class="tab-pane" id="patch" markdown="1">
Patch allows you to export only selected components of a solution for minor updates, giving you tighter control through segmentation without needing to export entire entities.  
Format : MAJOR.MINOR.BUILD.REVISION  
* Create a solution patch (cloner un correctif) and `only increment the revision number` to include the elements you want to update
* `Never use clone a solution`
* `The revision number must increase for each patch`
* `Increment the build number when redeploying the parent solution`  
Make sure to follow this versioning structure :
```
Parent : 1.1.0.0
    Clone patch 1 : 1.1.0.1 → Deploy to PROD
    Delete patch 1
    Clone patch 2 : 1.1.0.2 → Deploy to PROD
    Delete patch 2
    
    Deploy parent to PROD : 1.1.1.0
```
</div>

<div role="tabpanel" class="tab-pane" id="power-bi" markdown="1">    
### Embed Power BI report on the dashboard
<a href="https://learn.microsoft.com/en-us/power-apps/user/add-powerbi-dashboards" target="_blank" rel="noopener noreferrer">Power BI dashboard</a>

### Embed Power BI report in a form
#### Method 1: Built-in PowerBI control
<a href="https://learn.microsoft.com/en-us/power-apps/maker/model-driven-apps/embed-powerbi-report-in-system-form" target="_blank" rel="noopener noreferrer">Power BI form</a>  
#### Method 2: HTML WebResource with custom framework
</div>

<div role="tabpanel" class="tab-pane" id="power-fx" markdown="1">
## 📚 Documentation
* <a href="https://learn.microsoft.com/en-us/power-platform/power-fx/formula-reference-model-driven-apps" target="_blank" rel="noopener noreferrer">Formula reference</a>
* Use '&' to concatenate string
#### Switch
```
Switch(
Value(field);
917140001;"text1";
917140002;"text2";
917140003;"text3";""
)
```
</div>

<div role="tabpanel" class="tab-pane" id="pfc" markdown="1">
## 📚 Documentation
* <a href="https://github.com/LucasHahne/PCF-MDA-Background-Control?tab=readme-ov-file" target="_blank" rel="noopener noreferrer">Background control</a>
</div>

<div role="tabpanel" class="tab-pane" id="sharepoint" markdown="1">
## 📚 Documentation
* <a href="https://jscheper.com/single-sharepoint-document-storage-sales-process/?utm_source=substack&utm_medium=email" target="_blank" rel="noopener noreferrer">Centralized SharePoint storage for sales documents</a>
* <a href="https://www.youtube.com/watch?v=KVnfWsO1Fhs" target="_blank" rel="noopener noreferrer">
Create dynamic word</a>

## Common issues
* If the SharePoint folder location is based on a parent entity, verify that the “Based on entity” option is not selected in both the new UI and the classic UI
</div>

<div role="tabpanel" class="tab-pane" id="copilot" markdown="1">
## 📚 Documentation
* <a href="https://www.microsoft.com/en-us/power-platform/blog/power-apps/customize-model-driven-forms-to-leverage-copilot-studio-content-preview/" target="_blank" rel="noopener noreferrer">Customize model-driven forms to leverage Copilot</a>
</div>
</div>
{% include links.html %}
