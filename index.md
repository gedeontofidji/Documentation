---
title: "PowerBI"
keywords: dataverse
sidebar: mydoc_sidebar
permalink: index.html
summary: PowerBI.
---

{% include note.html content="I have a blog on technical writing here called <a alt='technical writing blog' href='http://idratherbewriting.com'>I'd Rather Be Writing</a>." %}

## General

Follow these instructions to build the theme.

### 1. Download the theme

First, download or clone the theme from the [Github repo](https://github.com/tomjoht/documentation-theme-jekyll). Most likely you won't be pulling in updates once you start customizing the theme, so downloading the theme (instead of cloning it) probably makes the most sense. In Github, click the **Clone or download** button, and then click **Download ZIP**.

## Dataverse connector
If you encounter the error `The type of the current preview value is too complex to display` when loading a Dataverse table into Power BI, it usually means Power Query is trying to display complex objects such as nested records or navigation properties. To avoid this, you can simplify the query by :
```
=
let Source = CommonDataService.Database("organisationName.crm4.dynamics.com"),
SelectedTable = Source{[Schema="dbo", Item="tableName"]}[Data],
SelectedColumns = Table.SelectColumns(SelectedTable, {"column1","column2", "column3"})
in SelectedColumns
```
