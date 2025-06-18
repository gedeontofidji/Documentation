The 'people' layout displays a list of persons with additional information. Typically, this layout is well suited for a people directory.

!["People layout"](../../../assets/webparts/search-results/layouts/people_layout.png){: .center}

 By default, the user profile pictures are fetched from the SharePoint User Profile Service. If your user pictures are stored in Active Directoy, make sure you synchronized them with SharePoint User Profile Service. Otherwise, a placeholder image will be displayed.

| Setting | Description | Default value 
| ------- |---------------- | ----------
| **Manage people fields** | Allows you to define you own values for people placeholder fields. <br><p align="center">[!["Manage people fields"](../../../assets/webparts/search-results/layouts/manage_people_fields.png)](../../../assets/webparts/search-results/layouts/manage_people_fields.png)</p>As a field value, you can choose either a field property value (from the list or as free text) and without any transformation or use an Handlebars expression by clicking on the checkbox next to it. In this case, all helpers from the main template are available. Also, if the field doesn't have the **'Allow HTML'** indication flag enabled, it means the value will be always interpreted as text, regardless if you set an HTML value. Otherwise, your value will be interpreted as HTML for those fields (ex: '_Primary text_' placeholder field). For HTML fields you can use the special variable `@root.theme` to use theme colors (ex: `@root.theme.palette.themePrimary`) or `@root.slots.<SlotName>` to access slot value. If you don't set a value for those fields (i.e an empty value), they won't appear in the UI.<br><br>The _UPN_-field is necessary for displaying the native SharePoint persona card (LPC) on hover. If it contains an empty value, the persona card will not appear. If it contains an invalid value (e.g. a non-existing UPN), an empty persona card will be shown.</br>
| **Show persona card on hover (LPC)** | If enabled, show a person card on hover for the curren item using the native SharePoint implementation.
| **Show persona card on hover** | If enabled, show a person card on hover for the current item. <p align="center">[!["Persona card hover"](../../../assets/webparts/search-results/layouts/persona_card_hover.png)](../../../assets/webparts/search-results/layouts/persona_card_hover.png)</p> This feature uses Microsoft Graph and [Microsoft Graph Toolkit](https://docs.microsoft.com/en-us/graph/toolkit/components/person) to display information about the user and needs the following API permissions in your tenant to work: <ul><li>User.Read</li><li>People.Read</li><li>Contacts.Read</li><li>User.Read.All</li></ul>**If these permissions are not set, the card won't appear**. You can use [PnP Office 365 CLI](https://pnp.github.io/office365-cli/cmd/spo/serviceprincipal/serviceprincipal-grant-add/) to add correct permissions for this feature:</br></br>`$m365 spo serviceprincipal grant add --resource '<aad_app_display_name>' --scope 'user_impersonation'`. Refer to the section below about [persona hover card customization](#persona-hover-card).
| **Show presence** | <p>If enabled, the person's presence-information will be displayed in the bottom right corner of the user's profile picture.</p>This feature uses Microsoft Graph and needs the API permission 'Presence.Read.All' in your tenant to work.
| **Component size** | The size of the person item (not only the picture). The more the size is and the more information will be displayed for each item and vice versa.

#### Persona hover card

Activating this option may slightly reduce loading performances because the user information are fecthed individually for each user (i.e result). **This option shouldn't be used with large page count.**

##### Microsoft Graph Toolkit

 The hover card uses Microsoft Graph Toolkit. This means you can add additional information providing your own template like this:

```html
 <mgt-person-card inherit-details>
    <template data-type="additional-details">
    <h3>Stuffed Animal Friends:</h3>
    <ul>
        <li>Giraffe</li>
        <li>lion</li>
        <li>Rabbit</li>
    </ul>
    </template>
</mgt-person-card>
```
More information [here](https://docs.microsoft.com/en-us/graph/toolkit/components/person-card).
