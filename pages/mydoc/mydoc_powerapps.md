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
    <li class="active"><a class="noCrossRef" href="general" data-toggle="tab">General</a></li>
    <li><a class="noCrossRef" href="#csharp" data-toggle="tab">C#</a></li>
    <li><a class="noCrossRef" href="#dynamics-app-outlook" data-toggle="tab">Dynamics 365 App for Outlook</a></li>
    <li><a class="noCrossRef" href="#javascript" data-toggle="tab">JavaScript</a></li>
    <li><a class="noCrossRef" href="#mscrm" data-toggle="tab">Mscrm - Document Core Pack</a></li>
    <li><a class="noCrossRef" href="#plan-designer" data-toggle="tab">Plan Designer</a></li>
    <li><a class="noCrossRef" href="#power-bi" data-toggle="tab">Power BI</a></li>
    <li><a class="noCrossRef" href="#power-fx" data-toggle="tab">Power Fx</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="general" markdown="1">
## 📚 Documentation
* [Restricted tables requiring Dynamics 365 licenses](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/data-platform-restricted-entities)
* [Virtual tables and lookup columns](https://mattruma.com/adventures-with-dataverse-virtual-tables-and-look-up-columns/?utm_source=substack&utm_medium=email)

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

#### 🙈 Hide “Create new” on a lookup field
* <a href="https://orby.com.au/hide-new-button-on-lookup-controls-in-model-driven-apps-2/" target="_blank" rel="noopener noreferrer">Hide new button on lookup controls</a> 
</div>

<div role="tabpanel" class="tab-pane" id="csharp" markdown="1">
### ⌘ Keyboard shortcuts (Microsoft Visual Studio)  
* Ctrl+Shift+B: Build the solution
* Ctrl+Space: Autocompletion  
    
#### Coding process
`The logic must be implemented in the service (e.g: CodeNasService) and not in the class (e.g: CodeNas).`  
Pull the code stored on Git > Add your code and save it > Build the solution to generates the DLL in the solution folder > Open Plugin Registration in XrmToolBox to update the plugin with the generated DLL > Test > Enter the commit message, then push the changes to Git.
{% include image.html file="visualstudio_pull.png" max-width="30%" %}
{% include image.html file="visualstudio_push.png" max-width="30%" %}

### Functions
* To create an entity: UserService.Create(entity)
</div>

<div role="tabpanel" class="tab-pane" id="dynamics-app-outlook" markdown="1">
#### Plugin that connects Outlook with Power Apps, allowing users to track emails or appointments, access or create records directly from Outlook.
* To enable integration of a table with Outlook, make sure the table is included in the 'Dynamics 365 App for Outlook' application.
* To install the plugin on a email user, go in the `application > Advanced setting > Email configuration > Mailboxes` and follow the steps :
{% include image.html file="powerapps_outlookconfiguration.png" max-width="50%" %}
</div>

<div role="tabpanel" class="tab-pane" id="javascript" markdown="1">
### 📚 Documentation
* <a href="https://learn.microsoft.com/en-us/power-apps/developer/model-driven-apps/clientapi/reference" target="_blank" rel="noopener noreferrer">JavaScript API</a>  

### 🐞 Debugging
Press `Fn+f12` to open the debug console.  
To help you debug during development, you can either display an alert popup on the UI,write a message in the debug console or stop the code at a specific moment:
```
alert("Field update with succes with the value :" + nom); OR
console.log("Field update with succes with the value :" + nom ); OR
debugger;
```

### 🗂️ Tab functions
To check whether a tab is open or closed. `Return a string: expanded or collapsed`
```
formContext.ui.tabs.get("TabName").getDisplayState();
if (formContext.ui.tabs.get("TabName").getDisplayState() === "collapsed");
```

### 🔀 Multi-select option set functions
Show/hide fields based on multi-select option set
```
function visibleField(executionContext)
{
    var formContext = executionContext.getFormContext();
    var optionSet = formContext.getAttribute("cto_myoptionset").getValue(); // Returns an array of integers (selected values)
    
    var control = formContext.getControl("cto_fieldtohideorshow");

    // First hide the field
    control.setVisible(false);

    // Replace '123456' with the actual integer value (option set value) you're checking for
    if (optionSet && optionSet.includes(123456))
    {
        control.setVisible(true);
    }
}
```

### ⚡ Quick create
Get the parent record reference of a quick create form
```
Xrm.Utility.getPageContext().input.data.parentrecordtype;
Xrm.Utility.getPageContext().input.createFromEntity.entityType;
```
{% include image.html file="quickcreateformparent.png" max-width="60%" %}

* <a href="/Documentation/javascript/shared_form.js" target="_blank" rel="noopener noreferrer">Form functions</a>
</div>

<div role="tabpanel" class="tab-pane" id="mscrm" markdown="1">
d
</div>

<div role="tabpanel" class="tab-pane" id="plan-designer" markdown="1">
#### Plan designer helps to create a plan for your existing solution or a new one using copilot. It generates a detailed document that describes your solution. The plan covers the business problem, user requirements like user roles and stories, the data model, and technologies like apps. This feature saves time when you're trying to understand a solution's content and helps makers improve an existing solution.

#### Documentation
* [Overview of Plan Designer](https://learn.microsoft.com/en-us/power-apps/maker/plan-designer/plan-designer)
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
