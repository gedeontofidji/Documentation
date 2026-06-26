---
title:  Useful links
keywords:
sidebar: mydoc_sidebar
permalink: mydoc_sharepoint_general.html
toc: false
folder: mydoc
---

<ul id="profileTabs" class="nav nav-tabs">
    <li class="active"><a class="noCrossRef" href="#general" data-toggle="tab">General</a></li>
    <li><a class="noCrossRef" href="#api" data-toggle="tab">API</a></li>
    <li><a class="noCrossRef" href="#content-type" data-toggle="tab">Content Type</a></li>
    <li><a class="noCrossRef" href="#list-library" data-toggle="tab">List and Library</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="general" markdown="1">
## Useful links
* [Admin Center](https://go.microsoft.com/fwlink/?linkid=2185220)

## Change the interface language
1. Select your name or profile on a Sharepoint Site
2. Select My profile Office => Update profile
3. Select the link “here”
4. Modify the language in the section Language and region”
</div>

<div role="tabpanel" class="tab-pane" id="api" markdown="1">
## Documentation
* <a href="https://learn.microsoft.com/en-us/sharepoint/dev/sp-add-ins/working-with-lists-and-list-items-with-rest" target="_blank" rel="noopener noreferrer">REST API</a>

All the methods are not documented, here is the way to find every existing action:
1. Go to `https://{site_url}/_api/$metadata` to view the complete XML data structure
2. Search for an object, for example: `<EntityType Name="Web"`. Inside, you will find its properties and relations to other objects through `<NavigationProperty>`
3. For example, the `Web` object is linked to `List`. You can find the details of that connected object by searching for `<EntityType Name="List"`
4. Combining these links gives you your API path, such as `_api/web/lists`
5. The `<Key>` property specifies the exact identifier required to target a specific object.
6. You can then select only the properties you want by appending `?$select=Name` to your request.
To perform actions on objects search for `<FunctionImport>` tags. For example, `<FunctionImport Name="Publish` shows that it applies to a File object and have one parameter. The query will look like :
* URL : _api/web/lists(guid'...')/items(42)/File/Publish
* Body : {"comment": "Your comment here"}
```
<FunctionImport Name="Publish" IsBindable="true">
<Parameter Name="this" Type="SP.File"/>
<Parameter Name="comment" Type="Edm.String"/>
</FunctionImport>
```
</div>

<div role="tabpanel" class="tab-pane" id="content-type" markdown="1">
## Content Type
💡Tips
* When you update a content type template with the same name, you should run a sript to clean Office data cache on computers of users. It will avoid warming messages.
</div>

<div role="tabpanel" class="tab-pane" id="list-library" markdown="1">
## Documentation
* [View formatting](https://learn.microsoft.com/en-us/sharepoint/dev/declarative-customization/view-formatting)
* <a href="https://manueltgomes.com/microsoft/sharepoint/translate-your-list-column-names/" target="_blank" rel="noopener noreferrer">Columns translations</a>
* [Formula columns](https://support.microsoft.com/fr-fr/office/exemples-de-formules-courantes-dans-des-listes-d81f5f21-2b4e-45ce-b170-bf7ebf6988b3)
* [Breaking the inheritance of permissions on a folder](https://support.microsoft.com/fr-fr/office/modifier-les-autorisations-sur-un-sous-dossier-5427bd7c-f20a-4f75-8cf2-5359dd45a1a6)
* <a href="https://www.linkedin.com/pulse/int%C3%A9grer-le-champ-n-de-version-sharepoint-dans-un-quickpart-dorigo-nhw3c?utm_source=share&utm_medium=member_android&utm_campaign=share_via" target="_blank" rel="noopener noreferrer">Display sharepoint version in a word document</a>
</div>
</div>

{% include links.html %}
