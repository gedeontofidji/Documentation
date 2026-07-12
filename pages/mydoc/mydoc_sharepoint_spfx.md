---
title:  SPFx
keywords:
sidebar: mydoc_sidebar
permalink: mydoc_sharepoint_spfx.html
toc: false
folder: mydoc
---

<ul id="profileTabs" class="nav nav-tabs">
    <li class="active"><a class="noCrossRef" href="#general" data-toggle="tab">General</a></li>
    <li><a class="noCrossRef" href="#fieldCustomizer" data-toggle="tab">Field Customizer</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="general" markdown="1">
SPFx is JavaScript running directly in the browser. Microsoft implemented a complete framework using this stack:
* TypeScrit: a typed JavaScript that compiles into JavaScript
* Node.js: enable running JavaScript on server and running scripts such as yeoman(yo)/heft
* VS Code
* Libraries: React (UI), Pnp (SP interactions), Office UI Fabric (graphic components like buttons and lists)
    
Managing this entire stack can lead to version compatibility issues between tools. To resolve this, you can use <a href="https://www.nvmnode.com/fr/" target="_blank" rel="noopener noreferrer">NVM</a> to switch between multiple Node.js versions. `If Node.js is already installed, you must uninstall it before installing NVM`

### ⌨️ Commands & Shortcuts
* `Ctrl + J`: open terminal
* `node -v`: get the installed version of Node.js
* `npm list @rushstack/heft`: get the installed version of heft
* `npm -v @microsoft/generator-sharepoint`: get the installed version of SPFx
* <a href="https://www.nvmnode.com/cli/" target="_blank" rel="noopener noreferrer">NVM commands</a>
* `heft clean` (optional) --> `heft build --production` (compile TypeScript into JavaScript) --> `heft package-solution --production` (create the sppkg package in sharepoint/solution folder)

### 🛠️ Create the App Catalog on a SharePoint site using PowerShell
1. `Get-Module -ListAvailable -Name Microsoft.Online.SharePoint.PowerShell`: check if the module is already installed
2. `Install-Module -Name Microsoft.Online.SharePoint.PowerShell -Scope CurrentUser`
3. `Import-Module Microsoft.Online.SharePoint.PowerShell`
4. `Connect-SPOService -Url https://tenantName-admin.sharepoint.com`: connection using admin acess
5. `Add-SPOSiteCollectionAppCatalog -Site https://tenantName.sharepoint.com/sites/siteName`: create the App Catalog for the target site
</div>

<div role="tabpanel" class="tab-pane" id="fieldCustomizer" markdown="1">
1. Create a folder and open it in VS Code
2. Run `yo @microsoft/sharepoint`
{% include image.html file="sharepoint_fieldCustomizer.png" max-width=""85%" %}

### 🔌 Link a component to a SharePoint field
1. Run this query to get the required field properties: `https://tenantName.sharepoint.com/sites/siteName/_api/web/lists(guid'libraryId')/fields/getbyinternalnameortitle('fieldInternalName')?$select=ClientSideComponentId,InternalName,TypeAsString,Group`: to fill sharepoint/assets/elements.xml`
2. Update config files: `sharepoint/assets/elements.xml` and `src/extensions/componentName/componentNameFieldCustomizer.manifest.json`

If ClientSideComponentId isn't defined, you can do it with the following request:
{% include image.html file="sharepoint_editFieldCustomizer.png" max-width="65%" %}
</div>
</div>

{% include links.html %}
