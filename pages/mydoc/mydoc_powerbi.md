---
title: "PowerBI"
keywords: dataverse, powerbi
sidebar: mydoc_sidebar
permalink: mydoc_powerbi.html
toc: false
summary: PowerBI is a Microsofttool that allows users to visualize data, create interactive reports, and share insights across an organization. It connects to various data sources, enabling real-time data analysis.
---

<ul id="profileTabs" class="nav nav-tabs">
    <li class="active"><a class="noCrossRef" href="#documentation" data-toggle="tab">Documentation</a></li>
    <li><a class="noCrossRef" href="#best-practice" data-toggle="tab"><Model best practices</a></li>
    <li><a class="noCrossRef" href="#dataverse-connection" data-toggle="tab">Dataverse connection</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="documentation" markdown="1">
* <a href="https://learn.microsoft.com/en-us/power-bi/collaborate-share/service-share-dashboards" target="_blank" rel="noopener noreferrer">Share Power BI reports with coworkers</a>
* <a href="https://learn.microsoft.com/en-us/power-bi/enterprise/service-premium-per-user-faq" target="_blank" rel="noopener noreferrer">Premium license</a>
</div>

<div role="tabpanel" class="tab-pane" id="best-practice" markdown="1">
* Organize datas by creating a `_rawDatas` folder to store all implicit measures
* Replace implicit measures automatically created by PowerBI with explicit DAX measures for better clarity and control
{% include image.html file="powerbi_rawdatas.png" max-width="100%" %}
</div>

<div role="tabpanel" class="tab-pane" id="dataverse-connection" markdown="1">
### Dataverse connector  
If you encounter the error `The type of the current preview value is too complex to display` when loading a Dataverse table into Power BI, it usually means Power Query is trying to display complex objects such as nested records or navigation properties. To avoid this, you can simplify the query by :
```
= let Source = CommonDataService.Database("organisationName.crm4.dynamics.com"),
SelectedTable = Source{[Schema="dbo", Item="tableName"]}[Data],
SelectedColumns = Table.SelectColumns(SelectedTable, {"column1","column2", "column3"})
in SelectedColumns
```

### Retrieve option set piclist
Option sets are not stored in any standard Dataverse table. To retrieve them, you need to use the Web connector with the following query :
```
= let Source = Json.Document(Web.Contents("https://organisationName.api.crm4.dynamics.com/api/data/v9.0/GlobalOptionSetDefinitions")),
TableBase = Table.FromRecords({Source}),
ExpandValue=Table.ExpandListColumn(TableBase,"value"),
ExpandFields=Table.ExpandRecordColumn(ExpandValue,"value",{"Name","Description","DisplayName","IsCustomizable","Options"}),
ExpandDescription=Table.ExpandRecordColumn(ExpandFields,"Description",{"UserLocalizedLabel"},{"Description.UserLocalizedLabel"}),
ExpandDisplayName=Table.ExpandRecordColumn(ExpandDescription,"DisplayName",{"UserLocalizedLabel"},{"DisplayName.UserLocalizedLabel"}),
ExpandIsCustomizable=Table.ExpandRecordColumn(ExpandDisplayName,"IsCustomizable",{"Value"}), ExpandOptions=Table.ExpandListColumn(ExpandIsCustomizable,"Options"),
ExpandOptionsLabel=Table.ExpandRecordColumn(ExpandOptions,"Options",{"Label","Value"},{"Label", "Options.Value"}),
ExpandOptionsUserLabel=Table.ExpandRecordColumn(ExpandOptionsLabel,"Label",{"UserLocalizedLabel"},{"Options.Label.UserLocalizedLabel"}),
ExpandLabel=Table.ExpandRecordColumn(ExpandOptionsUserLabel,"Options.Label.UserLocalizedLabel",{"Label"},{"Options.Label.UserLocalizedLabel.Label"}),
FinalTable=Table.RemoveColumns(ExpandLabel,{"Description.UserLocalizedLabel","DisplayName.UserLocalizedLabel","@odata.context","Value"})
in FinalTable
```
</div>
</div>

{% include links.html %}
