---
title:  Navtabs
keywords: navigation tabs, hide sections, tabbers, interface tabs
summary: "Navtabs provide a tab-based navagation directly in your content, allowing users to click from tab to tab to see different panels of condtent. Navtabs are especially helpful for showing code samples for different programming languages. The only downside to using navtabs is that you must use HTML instead of Markdown."
sidebar: mydoc_sidebar
permalink: mydoc_xrmtoolbox_tool.html
folder: mydoc
---


## Common uses

Navtabs are particularly useful for scenarios where you want to show a variety of options, such as code samples for Java, .NET, or PHP, on the same page.

While you could resort to single-source publishing to provide different outputs for each unique programming language or role, you could also use navtabs to allow users to select the content you want.

Navtabs are better for SEO since you avoid duplicate content and drive users to the same page.

## Navtabs demo

The following is a demo of a navtab. Refresh your page to see the tab you selected remain active.

<ul id="profileTabs" class="nav nav-tabs">
    <li class="active"><a class="noCrossRef" href="#profile" data-toggle="tab">Profile</a></li>
    <li><a class="noCrossRef" href="#about" data-toggle="tab">About</a></li>
    <li><a class="noCrossRef" href="#match" data-toggle="tab">Match</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="profile" markdown="1">
## Profile

Praesent sit amet fermentum leo. Aliquam feugiat, 

1.  nibh in u ltrices mattis
2.  felis ipsum venenatis metus, vel vehicula libero mauris a enim. Sed placerat est ac lectus vestibulum tempor. 
    * Quisque ut condimentum massa. 
    * ut condimentum massa. 

> Proin venenatis leo id urna cursus blandit. Vivamus sit amet hendrerit metus.
</div>

<div role="tabpanel" class="tab-pane" id="about">
    <h2>About</h2>
    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam vel sollicitudin felis. Sed eu arcu sed ipsum semper luctus eu a tortor. Suspendisse id leo eu metus laoreet varius. Mauris consequat accumsan ex, a iaculis metus fermentum a. Praesent sit amet fermentum leo. Aliquam feugiat, nibh in u ltrices mattis, felis ipsum venenatis metus, vel vehicula libero mauris a enim. Sed placerat est ac lectus vestibulum tempor. Quisque ut condimentum massa. Proin venenatis leo id urna cursus blandit. Vivamus sit amet hendrerit metus.about</p></div>

<div role="tabpanel" class="tab-pane" id="match">
    <h2>Match</h2>
    <p>Vel vehicula libero mauris a enim. Sed placerat est ac lectus vestibulum tempor. Quisque ut condimentum massa. Proin venenatis leo id urna cursus blandit. Vivamus sit amet hendrerit metus.</p>
</div>
</div>

## Code

Here's the code for the above (with the filler text abbreviated):

```html
<ul id="profileTabs" class="nav nav-tabs">
    <li class="active"><a href="#profile" data-toggle="tab">Profile</a></li>
    <li><a href="#about" data-toggle="tab">About</a></li>
    <li><a href="#match" data-toggle="tab">Match</a></li>
</ul>
  <div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="profile">
    <h2>Profile</h2>
<p>Praesent sit amet fermentum leo....</p>
</div>

<div role="tabpanel" class="tab-pane" id="about">
    <h2>About</h2>
    <p>Lorem ipsum ...</p></div>

<div role="tabpanel" class="tab-pane" id="match">
    <h2>Match</h2>
    <p>Vel vehicula ....</p>
</div>
</div>
```

## Match up ID tags

Each tab's `href` attribute must match the `id` attribute of the tab content's `div` section. So if your tab has `href="#acme"`, then you add `acme` as the ID attribute in `<div role="tabpanel" class="tab-pane" id="acme">`.


By setting a cookie, if the user refreshes the page, the active tab is the tab the user last selected (rather than defaulting to the default active tab).

{% include links.html %}
