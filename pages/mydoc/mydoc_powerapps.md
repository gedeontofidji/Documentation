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
    <li class="active"><a class="noCrossRef" href="#documentation" data-toggle="tab">Documentation</a></li>
    <li><a class="noCrossRef" href="#csharp" data-toggle="tab">C#</a></li>
    <li><a class="noCrossRef" href="#dynamics-app-outlook" data-toggle="tab">Dynamics 365 App for Outlook</a></li>
    <li><a class="noCrossRef" href="#javascript" data-toggle="tab">JavaScript</a></li>
    <li><a class="noCrossRef" href="#mscrm" data-toggle="tab">Mscrm - Document Core Pack</a></li>
    <li><a class="noCrossRef" href="#plan-designer" data-toggle="tab">Plan Designer</a></li>
    <li><a class="noCrossRef" href="#power-fx" data-toggle="tab">Power Fx</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="documentation" markdown="1">
## Documentation
* [Restricted tables requiring Dynamics 365 licenses](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/data-platform-restricted-entities)
* [Virtual tables and lookup columns](https://mattruma.com/adventures-with-dataverse-virtual-tables-and-look-up-columns/?utm_source=substack&utm_medium=email)  
</div>

<div role="tabpanel" class="tab-pane" id="csharp" markdown="1">
#### Csharp
Pull the code stored on Git > Add your code and save it > Build the solution to generates the DLL in the solution folder > Open Plugin Registration in XrmToolBox to update the plugin with the generated DLL > Test > Enter the commit message, then push the changes to Git.
{% include image.html file="visualstudio_pull.png" max-width="50%" %}
{% include image.html file="visualstudio_push.png"50%" %}
</div>

<div role="tabpanel" class="tab-pane" id="dynamics-app-outlook" markdown="1">
#### Plugin that connects Outlook with Power Apps, allowing users to track emails or appointments, access or create records directly from Outlook.
* To enable integration of a table with Outlook, make sure the table is included in the 'Dynamics 365 App for Outlook' application.
* To install the plugin on a email user, go in the `application > Advanced setting > Email configuration > Mailboxes` and follow the steps :
{% include image.html file="powerapps_outlookconfiguration.png" max-width="50%" %}
</div>

<div role="tabpanel" class="tab-pane" id="javascript" markdown="1">
* [JavaScript API](https://learn.microsoft.com/en-us/power-apps/developer/model-driven-apps/clientapi/reference)
To help you debug during development, you can either display an alert popup on the UI or write a message to the debug console:
```
alert("Field update with succes with the value :" + nom);
console.log("Field update with succes with the value :" + nom );
```
</div>

<div role="tabpanel" class="tab-pane" id="mscrm" markdown="1">
d
</div>

<div role="tabpanel" class="tab-pane" id="plan-designer" markdown="1">
#### Plan designer helps to create a plan for your existing solution or a new one using copilot. It generates a detailed document that describes your solution. The plan covers the business problem, user requirements like user roles and stories, the data model, and technologies like apps. This feature saves time when you're trying to understand a solution's content and helps makers improve an existing solution.

#### Documentation
* [Overview of Plan Designer](https://learn.microsoft.com/en-us/power-apps/maker/plan-designer/plan-designer)
</div>

<div role="tabpanel" class="tab-pane" id="power-fx" markdown="1">
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
</div>
{% include links.html %}
