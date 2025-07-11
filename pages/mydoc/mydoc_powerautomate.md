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
</div>

{% include links.html %}
