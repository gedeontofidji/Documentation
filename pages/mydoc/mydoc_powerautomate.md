---
title: "Power Automate"
keywords: power automate
sidebar: mydoc_sidebar
permalink: mydoc_powerautomate.html
toc: false
summary: Power Automate is a Microsoft tool that allows users to automate workflows, streamline repetitive tasks, and integrate apps and services across an organization. It connects to various data sources, enabling real-time process automation.
---

<ul id="profileTabs" class="nav nav-tabs">
    <li class="active"><a class="noCrossRef" href="#general" data-toggle="tab">General</a></li>
    <li><a class="noCrossRef" href="#common-issues" data-toggle="tab">Common issues</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="general" markdown="1">
## 📚 Documentation
* <a href="https://powerpro.nl/power-automate-check-user-role-for-authorization/?utm_source=substack&utm_medium=email" target="_blank" rel="noopener noreferrer">Check User Role for authorization</a>

## 🔍 Get the value of a lookup field with Dataverse connector
Write the following expression in a “Apply to each” loop :  
```
items('Apply_to_each')?[_my_field_value@OData.Community.Display.V1.FormattedValue]
```

## 📝 Set a lookup field with Dataverse connector
Write the following expression : /entityName(entityId)
{% include image.html file="powerautomate_setlookupfield.png" max-width="45%" %}
</div>

<div role="tabpanel" class="tab-pane" id="common-issues" markdown="1">
## Error 403 with a Service Principal flow connection  
To fix this issue, create the flow connection using PAC CLI (<a href="https://learn.microsoft.com/fr-fr/power-platform/developer/cli/introduction?tabs=windows" target="_blank" rel="noopener noreferrer">Install PAC</a>
```
pac auth create  https://organisationName.crm4.dynamics.com/
pac connection create -env https://organisationName.crm4.dynamics.com/ -t tenantId -a appId -cs clientSecret -n "Name of the application"
```
</div>
</div>

{% include links.html %}
