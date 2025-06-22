---
title: "PowerBI"
keywords: dataverse
sidebar: mydoc_sidebar
permalink: index.html
toc: false
summary: PowerBI.
---

<ul id="profileTabs" class="nav nav-tabs">
    <li class="active"><a class="noCrossRef" href="#webresources-manager" data-toggle="tab">Webresources Manager</a></li>
    <li><a class="noCrossRef" href="#plugin-trace-viewer" data-toggle="tab">Plugin Trace Viewer</a></li>
    <li><a class="noCrossRef" href="#match" data-toggle="tab">Match</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="webresources-manager" markdown="1">
## Webresources Manager

* Allows you to edit client-side code  
* Press Ctrl+S, then Ctrl+U to publish  
</div>

<div role="tabpanel" class="tab-pane" id="plugin-trace-viewer">
    <h2>Plugin Trace Viewer</h2>
    <ul>
      <li>Allows you to debug/view all server-side code executions</li>
    </ul>
</div>

<div role="tabpanel" class="tab-pane" id="match">
    <h2>Match</h2>
    <p>Vel vehicula libero mauris a enim. Sed placerat est ac lectus vestibulum tempor. Quisque ut condimentum massa. Proin venenatis leo id urna cursus blandit. Vivamus sit amet hendrerit metus.</p>
</div>
</div>

{% include links.html %}

## Dataverse connector
If you encounter the error `The type of the current preview value is too complex to display` when loading a Dataverse table into Power BI, it usually means Power Query is trying to display complex objects such as nested records or navigation properties. To avoid this, you can simplify the query by :
```
=
let Source = CommonDataService.Database("organisationName.crm4.dynamics.com"),
SelectedTable = Source{[Schema="dbo", Item="tableName"]}[Data],
SelectedColumns = Table.SelectColumns(SelectedTable, {"column1","column2", "column3"})
in SelectedColumns
```
