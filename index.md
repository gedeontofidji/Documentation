---
title: "PowerBI"
keywords: dataverse
sidebar: mydoc_sidebar
permalink: index.html
toc: false
summary: PowerBI is a Microsofttool that allows users to visualize data, create interactive reports, and share insights across an organization. It connects to various data sources, enabling real-time data analysis.
---

<ul id="profileTabs" class="nav nav-tabs">
    <li class="active"><a class="noCrossRef" href="#general" data-toggle="tab">General</a></li>
    <li><a class="noCrossRef" href="#dataverse-connector" data-toggle="tab">Dataverse connector</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="general" markdown="1">
## General

* Allows you to edit client-side code  
* Press Ctrl+S, then Ctrl+U to publish  
</div>

<div role="tabpanel" class="tab-pane" id="dataverse-connector" markdown="1">
## Dataverse connector  
If you encounter the error `The type of the current preview value is too complex to display` when loading a Dataverse table into Power BI, it usually means Power Query is trying to display complex objects such as nested records or navigation properties. To avoid this, you can simplify the query by :
```
=
let Source = CommonDataService.Database("organisationName.crm4.dynamics.com"),
SelectedTable = Source{[Schema="dbo", Item="tableName"]}[Data],
SelectedColumns = Table.SelectColumns(SelectedTable, {"column1","column2", "column3"})
in SelectedColumns
```
</div>
</div>

{% include links.html %}
