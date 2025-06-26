---
title: Power Apps
keywords: power apps
summary: "Power Apps is a Microsoft tool that allows users to build custom applications, simplify business processes, and enhance productivity across an organization. It connects to various data sources, enabling real-time data interaction and app development without extensive coding"
sidebar: mydoc_sidebar
toc: false
permalink: mydoc_powerapps.html
folder: mydoc
---

<ul id="profileTabs" class="nav nav-tabs">
    <li class="active"><a class="noCrossRef" href="#documentation" data-toggle="tab">Documentation</a></li>
    <li><a class="noCrossRef" href="#dynamics-app-outlook" data-toggle="tab">Dynamics 365 App for Outlook</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="documentation" markdown="1">
## Documentation
* [Restricted tables requiring Dynamics 365 licenses](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/data-platform-restricted-entities)
* [Virtual tables and lookup columns](https://mattruma.com/adventures-with-dataverse-virtual-tables-and-look-up-columns/?utm_source=substack&utm_medium=email)  
</div>

<div role="tabpanel" class="tab-pane" id="dynamics-app-outlook" markdown="1">
### Add-in that connects Outlook with Power Apps, allowing users to track emails or appointments, access or create records directly from Outlook.
* To enable integration of a table with Outlook through the plugin, make sure the table is included in the 'Dynamics 365 App for Outlook' application.
* To install the plugin on a email user, go in the `application > Advanced setting > Email configuration > Mailboxes` and follow the steps :
{% include image.html file="powerapps_outlookconfiguration.png" max-width="50%" %}
</div>
</div>

{% include links.html %}
