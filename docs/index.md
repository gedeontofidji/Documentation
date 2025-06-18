!["PnP"](./assets/pnp_logo.png){: .center .logo } 

# PnP Modern Search v4

![Current version](https://img.shields.io/github/v/release/microsoft-search/pnp-modern-search)

The PnP 'Modern Search' solution is a set of SharePoint Online modern Web Parts allowing SharePoint super users, webmasters and developers to create highly flexible and personalized search based experiences in minutes.

Before modern pages and web parts built on SPFx was introduced search driven scenarios was covered by the highly flexible classic search web parts, which
supported any developer to add any HTML, CSS or JavaScript they wanted to tailor their specific scenario. In the modern world this was replaced by the
Highlighted Content Web Part and a not very configurable search solution for Microsoft Search.

To close the gap of customization and freedom the PnP Modern Search web parts got started back in 2017, and have stabilized on v3. While allowing flexibility
it introduces security measures to block JavaScript and CSS injection, key to many of the enterprise companies using the web parts today in productions.

As the project progressed and the search API's are moving from SharePoint to Microsoft Graph there was a need to restructure and re-invent the web parts.
Hence v4 was born. The goal of v4 is to solve scenarios already solved by v3, but at the same time allow greater flexibility in how you extend the solution using web components and custom developer solutions outside of HTML/handlebars.

As more and more Microsoft Search functionality is exposed via the Microsoft Graph Search API's, we will keep on investing in v4 to surface these great capabilities.


## What's included?

The solution includes the following Web Parts:

| Component | Description |
| --------- | ----------- |
| **[Search Results](./usage/search-results/index.md)** | Retrieve data from a data source and render them in a specific layout.
| **[Search Filters](./usage/search-filters/index.md)** | Filter and refine data displayed in 'Search Results' Web Parts.
| **[Search Verticals](./usage/search-verticals/index.md)** | Browse data as silos (i.e. tabs) from multiple data sources.
| **[Search box](./usage/search-box/index.md)** | Let users enter free text queries sent to 'Search Results' Web Parts.

## Supported browsers

Here is the list of supported browsers:

- Chrome
- Firefox
- Edge
- Edge Chromium
- Brave

**PnP Modern Search do not explicitly support Internet Explorer 11**. We think there are plenty of other options for enterprise scenarios in the market. Maybe it's time to move on. For developers, it represents an **huge** amount of time to make the solution compatible for a very low benefit. Hope you understand, ain't personal ;).

## Extensibility model

By getting this solution, you also benefit from an advanced [extensibility model](./extensibility/index.md) allowing you to customize the solution according to your requirements if default features don't do the job for you.

**The supported extensions are:**

- [Custom layouts](./extensibility/custom_layout.md).
- [Custom web components](./extensibility/custom_web_component.md).
- [Custom Handlebars customization (helpers, partials, etc.)](./extensibility/handlebars_customizations.md).
- [Custom event handlers for adaptive cards actions](./extensibility/adaptivecards_customizations.md).
- [Custom query modifiers](./extensibility/custom_query_modifications.md).
- [Custom data sources](./extensibility/custom_data_sources.md).
- [Custom suggestions providers](./extensibility/custom_suggestions_provider.md).

With these available customizations options, you can do pretty much anything!

!!! note
    Extensibility samples are centralized in a dedicated repository: [https://github.com/microsoft-search/pnp-modern-search-extensibility-samples/tree/main](https://github.com/microsoft-search/pnp-modern-search-extensibility-samples/tree/main). Use them to get started to create your own or reuse existing samples in your projects.

## Troubleshooting

If you encounter an issue, please use the GitHub issues list of [this repository](https://github.com/microsoft-search/pnp-modern-search/issues). 

However, we will ask you to verify your issue as described here: [Using Query tools to verify issues](using-query-tools-to-verify-issues.md)

Also, to help us to resolve your issue, you can include screenshots or error messages coming from:

- The faulty Web Part itself.
- Errors displayed in the browser console (typically pressing F12).
- Errors displayed in the SharePoint console (pressing CTRL+F12)

## Issues, questions, feedback?
For any issue, question or feedback, please the [official GitHub repository](https://github.com/microsoft-search/pnp-modern-search/issues). We will be happy to help you!

## Q&A
We have a list of frequently asked questions available in our separate [Q&A section](QnA). If you have a question, it might be already answered there.

## About

PnP Modern Search version 4 initially made by [Franck Cornu](https://twitter.com/FranckCornu) based on a fork of the [@aequos 'Modern Data Visualizer'](https://www.aequos.ca/) solution.

## Maintainers & contributors

Here is the list of main contributors of the PnP Modern Search (all versions included)

- Mikael Svenson (Microsoft) - [@mikaelsvenson](http://www.twitter.com/mikaelsvenson)
- Franck Cornu (Ubisoft) - [@FranckCornu](http://www.twitter.com/FranckCornu)
- David Mehr (IOZAG) - [@davmehr](https://github.com/davemehr)
- Brad Schlintz (Microsoft) - [@bschlintz](https://twitter.com/bschlintz)
- Patrik Hellgren (SherparsGroupAB) - [@PatrikHellgren](https://twitter.com/patrikhellgren)
- Per Ove Sandhåland (crayon) - [@PerOve](https://github.com/PerOve)
- Marc Anderson ( Sympraxis) [@sympmarc](https://github.com/sympmarc)
- Fabio Franzini (Apvee Solutions) - [@franzinifabio](https://twitter.com/franzinifabio)
- Paolo Pialorsi (PiaSys.com) - [@PaoloPia](https://twitter.com/paolopia)
- Kasper Larsen (Fellowmind) - [@kasperbolarsen](https://twitter.com/kasperbolarsen)

 