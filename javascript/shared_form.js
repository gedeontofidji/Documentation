var Crm = Crm || {};
Crm.Utils = Crm.Utils || {};
Crm.Utils.Form = function ()
{
	function filterOptionSet(formContext, attribute, allValues, values)
	{
        /// Summary: 
        /// Filters an OptionSet control to display only the values specified in "values".
        /// Variables:
        ///   - formContext : object    → The form context.
        ///   - attribute   : string    → Logical name of the OptionSet attribute to be filtered.
        ///   - allValues   : Array     → List of all available options for the OptionSet control.
        ///   - values      : Array     → List of option values to display in the OptionSet control.
		var ctrl = formContext.getControl(attribute);
		var attr = formContext.getAttribute(attribute);
		if (!attr) return;
		var currentOption = attr.getValue();
		ctrl.clearOptions();
		for (let i = 0; i < allValues.length; i++)
		{
			if (values.includes(allValues[i].value))
			{
				ctrl.addOption(allValues[i]);
			}
		}
		if (values.includes(currentOption))
		{
			attr.setValue(currentOption);
		}
	}

	function getLookupValue(formContext, attribute)
	{
        /// Summary:
        /// Get the object representing the value of a lookup field.
        /// Variables:
        ///   - formContext : object    → The form context.
        ///   - attribute   : string    → Logical name of the lookup attribute.
		var attr = formContext.getAttribute(attribute);
		if (!attr) return null;
		var value = attr.getValue();
		if (value == null || value.length === 0) return null;
		value[0].id = value[0].id.replace("{", "").replace("}", "").toLowerCase();
		return value[0];
	}

	function setAttributesRequiredLevel(formContext, arrayOfAttributes, level)
	{
        /// Summary:
        /// Sets the required level for a list of attributes.
        /// Variables:
        ///   - formContext       : object    → The form context.
        ///   - level             : string    → Required level ("none", "recommended", or "required").
        ///   - arrayOfAttributes : string[]  → Array of attribute logical names.
		for (let i = 0; i < arrayOfAttributes.length; i++)
		{
			var attr = formContext.getAttribute(arrayOfAttributes[i]);
			if (attr) attr.setRequiredLevel(level);
		}
	}

	function setControlsVisibility(formContext, arrayOfControls, isVisible)
	{
        /// Summary:
        /// Sets the visibility of a list of controls.
        /// Variables:
        ///   - formContext     : object    → The form context.
        ///   - isVisible       : boolean   → Visibility flag (true or false).
        ///   - arrayOfControls : string[]  → Array of control names.
		for (let i = 0; i < arrayOfControls.length; i++)
		{
			var ctrl = formContext.getControl(arrayOfControls[i]);
			if (ctrl && ctrl.setVisible) ctrl.setVisible(isVisible);
		}
	}

	function setControlsDisabled(formContext, arrayOfAttributes, isDisabled)
	{
        /// Summary:
        /// Sets the read-only state of a list of controls.
        /// Variables:
        ///   - formContext     : object    → The form context.
        ///   - isDisabled      : boolean   → Read-only flag (true to disable, false to enable).
        ///   - arrayOfControls : string[]  → Array of control names.
		for (let i = 0; i < arrayOfControls.length; i++)
		{
			var attr = formContext.getAttribute(arrayOfControls[i]);
			if (!attr) continue;
			attr.controls.forEach(function (ctrl)
			{
				if (ctrl && ctrl.setDisabled) ctrl.setDisabled(isDisabled);
			});
		}
	}

	function setDateToday(formContext, attribute)
	{
        /// Summary:
        /// Sets the value of a date attribute to today's date with time set to midnight.
        /// Variables:
        ///   - formContext : object → The form context.
        ///   - attribute   : string → Logical name of the date attribute to set.
		var attr = formContext.getAttribute(attribute);
		if (!attr) return;
		if (attr.getAttributeType() !== "datetime") throw "L'attribut n'est pas de type datetime (Crm.Utils.Form.setDateToday)";
		var date = new Date();
		var today = date.setHours(0, 0, 0, 0);
		attr.setValue(new Date(today));
	}

	function setLookupValue(formContext, attribute, id, entityType, name)
	{
        /// Summary:
        /// Sets the value of a Lookup field on the form.
        /// Variables:
        ///   - formContext : object  → The form context.
        ///   - attribute   : string  → Logical name of the lookup attribute to set.
        ///   - id          : string  → ID of the record to link.
        ///   - entityType  : string  → Entity type name of the record to link.
        ///   - name        : string  → Display name of the record to link.
		var item = {
			id: id,
			entityType: entityType,
			name: name
		};
		var array = new Array();
		array.push(item);
		var attr = formContext.getAttribute(attribute);
		if (attr)
		{
			attr.setValue(array);
		}
	}

	function setSectionVisibility(formContext, tabName, sectionName, isVisible)
	{
        /// Summary:
        /// Sets the visibility of a section within a specific tab on the form.
        /// Variables:
        ///   - formContext : object   → The form context.
        ///   - tabName     : string   → The name of the tab containing the section.
        ///   - sectionName : string   → The name of the section to show/hide.
        ///   - isVisible   : boolean  → Visibility flag (true to show, false to hide).
		var tab = formContext.ui.tabs.get(tabName);
		if (!tab) return;
		var section = tab.sections.get(sectionName);
		if (!section) return;
		section.setVisible(isVisible);
	}

	function setTabsVisibility(formContext, tabsNames, isVisible)
	{
        /// Summary:
        /// Sets the visibility of multiple tabs on the form.
        /// Variables:
        ///   - formContext : object    → The form context.
        ///   - tabsNames   : string[]     → Array of tab names to show or hide.
        ///   - isVisible   : boolean   → Visibility flag (true to show, false to hide).
		for (let tabName of tabsNames)
		{
			var tab = formContext.ui.tabs.get(tabName);
			if (!tab) continue;
			tab.setVisible(isVisible);
		}
	}

	function checkUserSecurityRole(userId, roleName)
    {
        /// Summary:
        /// Checks if a user has a specific security role.
        /// Variables:
        ///   - userId      : string  → GUID of the system user.
        ///   - roleName    : string  → Name of the security role to check.
		return new Promise((successCallback, failureCallback) => {
			var fetchData = {
				"name": roleName,
				"systemuserid": userId
			};
			var fetchXml = [
            "?fetchXml=<fetch>",
            "  <entity name='role'>",
            "    <attribute name='name'/>",
            "    <filter>",
            "      <condition attribute='name' operator='in'>",
            "        <value>", fetchData.name, "</value>",
            "      </condition>",
            "    </filter>",
            "    <link-entity name='systemuserroles' from='roleid' to='roleid' intersect='true'>",
            "      <attribute name='systemuserid'/>",
            "      <filter>",
            "        <condition attribute='systemuserid' operator='eq' value='", fetchData.systemuserid, "'/>",
            "      </filter>",
            "    </link-entity>",
            "  </entity>",
            "</fetch>"
            ].join("");
			Xrm.WebApi.retrieveMultipleRecords("role", fetchXml).then
            (
                function success(result)
                {
                    if (result.entities.length > 0) successCallback(true);
                    else successCallback(false);
                },
			    function (error)
			    {
				console.log(error.message);
				failureCallback("Erreur");;
			    }
            );
		});
	}
    
    function refreshForm(formContext)
    {
        /// Summary:
        /// Reopens the current record form to simulate a full refresh of the entity data.
        /// Variables:
        ///   - formContext : object → The form context.
        var entityFormOptions = {
            entityName: formContext.data.entity.getEntityName(),
            entityId: formContext.data.entity.getId(),
            openInNewWindow: false
        };

        Xrm.Navigation.openForm(entityFormOptions).then
        (
            function (success) {
                console.log("Form refreshed with success:", success);
            },
            function (error) {
                console.error("Error while refreshing the form :", error.message);
            }
        );
    }

    function clearFieldOnChange(formContext,fieldToClear, eventType)
    {
        /// Summary:
        /// Clears the value of a field when the event type is "onChange".
        /// Variables:
        ///   - formContext   : object   → The form context.
        ///   - fieldToClear  : string   → Logical name of the field to clear.
        ///   - eventType     : string   → The event type (e.g., "onChange").
        if (eventType==="onChange")
        {
            formContext.getAttribute(fieldToClear).setValue(null);
        }
    }

    function getParentRecordFromQuickCreate()
    {
        /// Summary:
        /// Retrieves the parent record reference when the quick Create form is opened from another record.
        var context = Xrm.Utility.getPageContext();

        if (context.input && context.input.createFromEntity)
        {
            return {
                id: context.input.createFromEntity.id,
                name: context.input.createFromEntity.name,
                entityType: context.input.createFromEntity.entityType
            };
        }
        return null;
        /// Returns: object → { id, name, entityType } or null
    }

	return {
		FilterOptionSet: filterOptionSet,
		GetLookupValue: getLookupValue,
		SetAttributesRequiredLevel: setAttributesRequiredLevel,
		SetDateToday: setDateToday,
		SetLookupValue: setLookupValue,
		SetControlsDisabled: setControlsDisabled,
		SetControlsVisibility: setControlsVisibility,
		SetSectionVisibility: setSectionVisibility,
		SetTabsVisibility: setTabsVisibility,
		CheckUserSecurityRole: checkUserSecurityRole,
        ClearFieldOnChange:clearFieldOnChange,
        RefreshForm:refreshForm,
        GetParentRecordFromQuickCreate: getParentRecordFromQuickCreate,
	};
}();
