---
title:  XrmToolBox
keywords: xrmtoolbox, webresources
summary: "Navtabs provide a tab-based navagation directly in your content, allowing users to click from tab to tab to see different panels of condtent."
sidebar: mydoc_sidebar
permalink: mydoc_xrmtoolbox_tool.html
folder: mydoc
---


## Common uses

Navtabs are particularly useful for scenarios where you want to show a variety of options, such as code samples for Java, .NET, or PHP, on the same page. `href`

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
    <h2>About</h2>
    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam vel sollicitudin felis. Sed eu arcu sed ipsum semper luctus eu a tortor. Suspendisse id leo eu metus laoreet varius. Mauris consequat accumsan ex, a iaculis metus fermentum a. Praesent sit amet fermentum leo. Aliquam feugiat, nibh in u ltrices mattis, felis ipsum venenatis metus, vel vehicula libero mauris a enim. Sed placerat est ac lectus vestibulum tempor. Quisque ut condimentum massa. Proin venenatis leo id urna cursus blandit. Vivamus sit amet hendrerit metus.about</p></div>

<div role="tabpanel" class="tab-pane" id="match">
    <h2>Match</h2>
    <p>Vel vehicula libero mauris a enim. Sed placerat est ac lectus vestibulum tempor. Quisque ut condimentum massa. Proin venenatis leo id urna cursus blandit. Vivamus sit amet hendrerit metus.</p>
</div>
</div>

## Match up ID tags

Each tab's attribute must match the `id` attribute of the tab content's `div` section.

{% include links.html %}
