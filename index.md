---
title: "Getting started with the Documentation Theme for Jekyll"
keywords: sample homepage
sidebar: mydoc_sidebar
permalink: index.html
summary: These brief instructions will help you get started quickly with the theme. The other topics in this help provide additional information and detail about working with other aspects of this theme and Jekyll.
---

{% include note.html content="If you're cloning this theme, you're probably writing documentation of some kind. I have a blog on technical writing here called <a alt='technical writing blog' href='http://idratherbewriting.com'>I'd Rather Be Writing</a>." %}

## General

Follow these instructions to build the theme.

### 1. Download the theme

First, download or clone the theme from the [Github repo](https://github.com/tomjoht/documentation-theme-jekyll). Most likely you won't be pulling in updates once you start customizing the theme, so downloading the theme (instead of cloning it) probably makes the most sense. In Github, click the **Clone or download** button, and then click **Download ZIP**.

### 3. Install Bundler

In case you haven't installed Bundler, install it:

```
gem install bundler
```

You'll want [Bundler](http://bundler.io/) to make sure all the Ruby gems needed work well with your project. Bundler sorts out dependencies and installs missing gems or matches up gems with the right versions based on gem dependencies.

## Dataverse connector
If `The type of the current preview value is too complex to display` error appears when loading a dataverse table, write the query :
```
= let Source = CommonDataService.Database("cveboost.crm4.dynamics.com"),
TableOpportunity = Source{[Schema="dbo", Item="opportunity"]}[Data],
ColonnesChoisies = Table.SelectColumns(TableOpportunity, {"name","cve_familledeprojet", "cve_businessunit","cve_typemarche"})
in ColonnesChoisies
```
