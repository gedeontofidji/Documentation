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
    <li><a class="noCrossRef" href="#dataverse" data-toggle="tab">Dataverse</a></li>
    <li><a class="noCrossRef" href="#sharepoint" data-toggle="tab">SharePoint</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="general" markdown="1">
## 📚 Documentation
* <a href="https://powerpro.nl/power-automate-check-user-role-for-authorization/?utm_source=substack&utm_medium=email" target="_blank" rel="noopener noreferrer">Check User Role for authorization</a>
* <a href="https://www.youtube.com/watch?v=KVnfWsO1Fhs" target="_blank" rel="noopener noreferrer">
Create dynamic word</a>

## Functions reference
* `formatNumber(number, 'D3')` to convert an integer into a 3-digit number : <a href="https://learn.microsoft.com/en-us/azure/logic-apps/expression-functions-reference#formatNumber" target="_blank" rel="noopener noreferrer"> Microsoft doc</a>
* `(date gt '2025-01-01T00:00:00Z') and (date lt '2025-12-31T00:00:00Z')` to compare dates
</div>

<div role="tabpanel" class="tab-pane" id="common-issues" markdown="1">
* <a href="https://www.linkedin.com/pulse/escape-single-quotes-odata-filter-query-power-ian-bennett-/" target="_blank" rel="noopener noreferrer"> Fixing OData filter errors when strings contain apostrophes </a>
    
## Error 403 with a Service Principal flow connection  
To fix this issue, create the flow connection using PAC CLI (<a href="https://learn.microsoft.com/fr-fr/power-platform/developer/cli/introduction?tabs=windows" target="_blank" rel="noopener noreferrer">Install PAC</a>)
```
pac auth create  https://organisationName.crm4.dynamics.com/
pac connection create -env https://organisationName.crm4.dynamics.com/ -t tenantId -a appId -cs clientSecret -n "Name of the application"
```
</div>

<div role="tabpanel" class="tab-pane" id="dataverse" markdown="1">
### Expand a query `Always use the schema name of the lookup field `
```
cor_ProducteurDechetTiers($select=name,),owninguser($select=title,mobilephone)
```

### Get the value of a lookup field with Dataverse connector
Write the following expression in a “Apply to each” loop :  
```
items('Apply_to_each')?[_my_field_value@OData.Community.Display.V1.FormattedValue]
```

### Set a lookup field with Dataverse connector
Write the following expression : /entityPluralName(recordId)
{% include image.html file="powerautomate_setlookupfield.png" max-width="45%" %}
</div>

<div role="tabpanel" class="tab-pane" id="sharepoint" markdown="1">
## SharePoint - OData queries
* `FSObjType eq 0` to retrieve only files
* <a href="https://learn.microsoft.com/en-us/sharepoint/dev/business-apps/power-automate/guidance/working-with-get-items-and-get-files" target="_blank" rel="noopener noreferrer">Get items/files properties</a>
</div>

</div>
{% include links.html %}
