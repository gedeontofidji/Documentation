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
    <li><a class="noCrossRef" href="#other" data-toggle="tab">Other</a></li>
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
* `node -v`: get the installed version of Node.js
* `npm list @rushstack/heft`: get the installed version of heft
* `npm -v @microsoft/generator-sharepoint`: get the installed version of SPFx
* <a href="https://www.nvmnode.com/cli/" target="_blank" rel="noopener noreferrer">NVM commands</a>
* `heft clean` (optional) --> `heft build --production` (compile TypeScript into JavaScript) --> `heft package-solution --production` (create the sppkg package in sharepoint/solution folder)

### 🔗 Useful links
</div>

<div role="tabpanel" class="tab-pane" id="other" markdown="1">
## Other
</div>
</div>

{% include links.html %}
