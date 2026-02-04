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
    <li><a class="noCrossRef" href="#sharepoint" data-toggle="tab">SharePoint forms</a></li>
    <li><a class="noCrossRef" href="#sharepoint-patch" data-toggle="tab">SP patch syntax</a></li>
</ul>
  <div class="tab-content">
      
<div role="tabpanel" class="tab-pane active" id="general" markdown="1">
## 📚 Documentation
* <a href="https://learn.microsoft.com/en-us/power-apps/guidance/coding-guidelines/code-readability" target="_blank" rel="noopener noreferrer">Naming conventions</a>

## Code optimization (<a href="https://learn.microsoft.com/en-us/power-apps/guidance/coding-guidelines/code-optimization" target="_blank" rel="noopener noreferrer">Documentation</a>)
* Limit the size and number of collections. Use the `ShowColumns` function to get only specific columns and add the `Filter` function to get only relevant data.
* `Coalesce` function
* Enable DelayOutput for text controls
</div>

<div role="tabpanel" class="tab-pane" id="sharepoint" markdown="1">
## 📚 Documentation
* <a href="https://learn.microsoft.com/en-us/power-apps/maker/canvas-apps/sharepoint-form-integration" target="_blank" rel="noopener noreferrer">SharePoint forms integration</a>
* <a target="\_blank" class="noCrossRef" href="{{ "pdf/CustomFormsMigration.pdf"}}"><button type="button" class="btn btn-default" aria-label="Left Align"><span class="glyphicon glyphicon-download-alt" aria-hidden="true"></span> Best practices for SharePoint custom forms </button></a>

#### 🚀 Deploy SharePoint custom form from DEV to PROD
1. In DEV, go to List settings > Form settings > See versions and usage > Export package > Create as new
2. <a href="https://github.com/gedeontofidji/PowerShell/blob/main/CanvaApp/DeployFromDevToProd.ps1" target="_blank" rel="noopener noreferrer">Run this script in the folder where the exported package is</a>. `PowerShell may not run the script due to execution policy. In that case, create each script file manually by copying the content.`
4. In PROD, go to List settings > Form settings > Use the default SharePoint form > Delete custom form
5. Open PowerApp portal in the environment where you want to migrate the SharePoint form > Import canvas app
6. In PROD, open the SharePoint form and publish it
</div>

<div role="tabpanel" class="tab-pane" id="sharepoint-patch" markdown="1">
* <a href="https://www.youtube.com/watch?v=g9ChYuTdNd4&t=867s" target="_blank" rel="noopener noreferrer">YouTube tutorial</a>  
  
Yes/No column
```
 1  // Yes value. For No value use false
```

Choice column
```
{Value: "choice value"}
```

Person column
```
{
    Claims: Concatenate(
        "i:0#.f|membership|",
        User().Email // Person email
        ),
        Department: "",
        DisplayName: User().FullName,
        Email: User().Email, // Person email
        JobTitle: "",
        Picture: ""
}
```

Lookup column
```
{
    Value: "val1", // Value of lookup column
    Id: 2 // Id of lookup column
}
```

Multi select choice column
```
Table(
    {Value: "choice value 1"},
    {Value: "choice value 2"} // keep adding multiple choices
   )
```

Managed metadata  column
```
{
    Label: "label",
    Path: "",
    TermGuid: "d30784d3-f4dc-46e2-b3ed-577b9ae5bea9",// Replace with term guid
    Value: "",
    WssId: 0
}       
```
</div>

</div>
{% include links.html %}
