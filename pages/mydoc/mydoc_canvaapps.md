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
</ul>
  <div class="tab-content">
      
<div role="tabpanel" class="tab-pane active" id="general" markdown="1">
## 📚 Documentation
* <a href="https://learn.microsoft.com/en-us/power-apps/guidance/coding-guidelines/code-readability" target="_blank" rel="noopener noreferrer">Naming conventions</a>

## Code optimization (<a href="https://learn.microsoft.com/en-us/power-apps/guidance/coding-guidelines/code-optimization" target="_blank" rel="noopener noreferrer">Documentation</a>)
* Enable DelayOutput for text controls
</div>

<div role="tabpanel" class="tab-pane" id="sharepoint" markdown="1">
## 📚 Documentation
* <a href="https://learn.microsoft.com/en-us/power-apps/maker/canvas-apps/sharepoint-form-integration" target="_blank" rel="noopener noreferrer">SharePoint forms integration</a>
* <a target="\_blank" class="noCrossRef" href="{{ "pdf/CustomFormsMigration.pdf"}}"><button type="button" class="btn btn-default" aria-label="Left Align"><span class="glyphicon glyphicon-download-alt" aria-hidden="true"></span> Best practices for SharePoint custom forms </button></a>

#### ⚙️ Conditionally make form fields mandatory
1. Select the data card that contains the field you want to make mandatory
2. Go to the advanced properties and make the required property true or edit with your condition

#### 🔎 Lookup User field (<a href="https://rezadorrani.com/index.php/2020/05/04/power-apps-patch-function-with-sharepoint/" target="_blank" rel="noopener noreferrer">sharePoint picklist syntax</a>)
1. Add the data source Office365Users
2. Set Items property on `Office365Users.SearchUserV2({isSearchTermRequired:false; top:999}).value`
3. Set DefaultSelectedItems property
4. Set Update property with sharePoint syntax
</div>

</div>
{% include links.html %}
