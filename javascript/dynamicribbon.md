---
title:  XrmToolBox
keywords: ribbon
sidebar: mydoc_sidebar
permalink: javascript_dynamicribbon.html
toc: false
folder: javascript
---

<ul id="profileTabs" class="nav nav-tabs">
    <li class="active"><a class="noCrossRef" href="#bpf-manager" data-toggle="tab">BPF Manager</a></li>
    <li><a class="noCrossRef" href="#plugin-registration" data-toggle="tab">Plugin Registration</a></li>
</ul>
<div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="bpf-manager" markdown="1">
## Allows you to change BPFs on individual or multiple records
### 🪜 Steps
1. Select the records through a view or with FetchXMLBuilder
2. Retrieve the records
3. Select the target stage of BPF
4. Choose the Yes option
5. Migrate the records
{% include image.html file="bpfmanager.png" max-width="100%" %}
</div>

<div role="tabpanel" class="tab-pane" id="plugin-registration" markdown="1">
## Plugin Registration
#### Allows you to view and edit server-side code.  
`Asynchronous` : Blocks the user before saving  
`Synchronous` : Does not block the user before saving and runs after saving  
`Pre-image` : Captures data from a table before/after saving  
`Post-image` : Captures data from a table after saving  
</div>
</div>

{% include links.html %}
