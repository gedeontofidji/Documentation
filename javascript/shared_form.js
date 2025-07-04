var Crm = Crm || {};
Crm.Utils = Crm.Utils || {};
Crm.Utils.Form = function ()
{
	function disableFormSelector(formContext)
	{
		formContext.ui.formSelector.items.forEach(function (f)
		{
			f.setVisible(false);
		});
	}

	function filterOptionSet(formContext, attribute, allValues, values)
	{
		///<summary>
		/// Filtre un contrôle de type OptionSet en ne présentant que les valeurs dans "values"
		///</summary>
		///<param name="formContext" type="object">
		/// Contexte du formulaire.
		///</param>
		///<param name="attribute" type="String">
		/// Nom de l'attribut pour le contrôle OptionSet à filtrer.
		///</param>
		///<param name="allValues" type="Array">
		/// Liste de toutes les options du contrôle OptionSet.
		///</param>
		///<param name="values" type="Array">
		/// Liste des options à présenter dnas le contrôle OptionSet.
		///</param>
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
		///<summary>
		/// Obtient l'objet représentant la valeur d'un lookup
		///</summary>
		///<param name="formContext" type="object">
		/// Contexte du formulaire.
		///</param>
		///<param name="attribute" type="String">
		/// Nom de l'attribut Lookup.
		///</param>
		var attr = formContext.getAttribute(attribute);
		if (!attr) return null;
		var value = attr.getValue();
		if (value == null || value.length === 0) return null;
		value[0].id = value[0].id.replace("{", "").replace("}", "").toLowerCase();
		return value[0];
	}

	function getOptionSet(form, attribute)
	{
		var attr = form.getAttribute(attribute);
		if (!attr) return null;
		return {
			value: attr.getValue(),
			label: attr.getText()
		};
	}

	function setAttributesRequiredLevel(formContext, level, arrayOfAttributes)
	{
		///<summary>
		/// Définit le niveau requis d'une liste d'attributs
		///</summary>
		///<param name="formContext" type="object">
		/// Contexte du formulaire.
		///</param>
		///<param name="level" type="String">
		/// Niveau requis ("none"/"recommended"/"required").
		///</param>
		///<param name="arrayOfControls" type="String[]">
		/// Noms des contrôles.
		///</param>
		for (let i = 0; i < arrayOfAttributes.length; i++)
		{
			var attr = formContext.getAttribute(arrayOfAttributes[i]);
			if (attr) attr.setRequiredLevel(level);
		}
	}

	function setControlsVisibility(formContext, isVisible, arrayOfControls)
	{
		///<summary>
		/// Définit la visibilité d'une liste de contrôles
		///</summary>
		///<param name="formContext" type="object">
		/// Contexte du formulaire.
		///</param>
		///<param name="isVisible" type="Boolean">
		/// Indicateur de visibilité.
		///</param>
		///<param name="arrayOfControls" type="String[]">
		/// Noms des contrôles.
		///</param>
		for (let i = 0; i < arrayOfControls.length; i++)
		{
			var ctrl = formContext.getControl(arrayOfControls[i]);
			if (ctrl && ctrl.setVisible) ctrl.setVisible(isVisible);
		}
	}

	function setAttributesVisibility(formContext, isVisible, arrayOfControls)
	{
		///<summary>
		/// Définit la visibilité d'une liste de contrôles
		///</summary>
		///<param name="formContext" type="object">
		/// Contexte du formulaire.
		///</param>
		///<param name="isVisible" type="Boolean">
		/// Indicateur de visibilité.
		///</param>
		///<param name="arrayOfControls" type="String[]">
		/// Noms des contrôles.
		///</param>
		for (let i = 0; i < arrayOfControls.length; i++)
		{
			var attr = formContext.getAttribute(arrayOfControls[i]);
			if (!attr) continue;
			attr.controls.forEach(function (ctrl)
			{
				if (ctrl && ctrl.setVisible) ctrl.setVisible(isVisible);
			});
		}
	}

	function setControlsDisabled(formContext, isDisabled, arrayOfControls)
	{
		///<summary>
		/// Définit la visibilité d'une liste de contrôles
		///</summary>
		///<param name="formContext" type="object">
		/// Contexte du formulaire.
		///</param>
		///<param name="isDisabled" type="Boolean">
		/// Indicateur de lecture seule.
		///</param>
		///<param name="arrayOfControls" type="String[]">
		/// Noms des contrôles.
		///</param>
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
		///<summary>
		/// Définit la valeur d'un attribut date avec la date du jour
		///</summary>
		///<param name="formContext" type="object">
		/// Contexte du formulaire.
		///</param>
		///<param name="attribute" type="String">
		/// Nom de l'attribut Lookup à définir.
		///</param>
		var attr = formContext.getAttribute(attribute);
		if (!attr) return;
		if (attr.getAttributeType() !== "datetime") throw "L'attribut n'est pas de type datetime (Crm.Utils.Form.setDateToday)";
		var date = new Date();
		var today = date.setHours(0, 0, 0, 0);
		attr.setValue(new Date(today));
	}

	function setLookupValue(formContext, attribute, id, type, name)
	{
		///<summary>
		/// Définit une valeur d'un champ Lookup
		///</summary>
		///<param name="formContext" type="object">
		/// Contexte du formulaire.
		///</param>
		///<param name="attribute" type="String">
		/// Nom de l'attribut Lookup à définir.
		///</param>
		///<param name="id" type="String">
		/// Identifiant de l'enregistrement à lier.
		///</param>
		///<param name="type" type="String">
		/// Type de l'enregistrement à lier.
		///</param>
		///<param name="name" type="String">
		/// Nom d'affichage de l'enregistrement à lier.
		///</param>
		var item = {
			id: id,
			entityType: type,
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

	function setLookupValueFromRecord(formContext, attribute, record, sourceAttribute)
	{
		///<summary>
		/// Définit une valeur d'un champ Lookup
		///</summary>
		///<param name="formContext" type="object">
		/// Contexte du formulaire.
		///</param>
		///<param name="attribute" type="String">
		/// Nom de l'attribut Lookup à définir.
		///</param>
		///<param name="record" type="object">
		/// Identifiant de l'enregistrement à lier.
		///</param>
		///<param name="sourceAttribute" type="String">
		/// Type de l'enregistrement à lier.
		///</param>
		if (sourceAttribute.indexOf("_") != 0)
		{
			sourceAttribute = "_" + sourceAttribute + "_value";
		}
		if (!record[sourceAttribute]) return;
		var item = {
			id: record[sourceAttribute],
			entityType: record[sourceAttribute + "@Microsoft.Dynamics.CRM.lookuplogicalname"],
			name: record[sourceAttribute + "@OData.Community.Display.V1.FormattedValue"]
		};
		var array = [];
		array.push(item);
		var attr = formContext.getAttribute(attribute);
		if (attr)
		{
			attr.setValue(array);
		}
	}

	function setSectionVisibility(formContext, tabName, sectionName, isVisible)
	{
		///<summary>
		/// Définit la visibilité d'une section
		///</summary>
		///<param name="formContext" type="object">
		/// Contexte du formulaire.
		///</param>
		///<param name="tabName" type="String">
		/// Nom de l'onglet contenant la section.
		///</param>
		///<param name="sectionName" type="String">
		/// Nom de la section.
		///</param>
		///<param name="isVisible" type="Boolean">
		/// Indicateur de visibilité.
		///</param>
		var tab = formContext.ui.tabs.get(tabName);
		if (!tab) return;
		var section = tab.sections.get(sectionName);
		if (!section) return;
		section.setVisible(isVisible);
	}

	function setState(formContext, disabled)
	{
		formContext.ui.controls.forEach(function (ctrl)
		{
			if (ctrl.setDisabled) ctrl.setDisabled(disabled);
		});
	}

	function setTabVisibility(formContext, tabName, isVisible)
	{
		///<summary>
		/// Définit la visibilité d'une section
		///</summary>
		///<param name="formContext" type="object">
		/// Contexte du formulaire.
		///</param>
		///<param name="tabName" type="String">
		/// Nom de l'onglet.
		///</param>
		///<param name="isVisible" type="Boolean">
		/// Indicateur de visibilité.
		///</param>
		var tab = formContext.ui.tabs.get(tabName);
		if (!tab) return;
		tab.setVisible(isVisible);
	}

	function setTabsVisibility(formContext, tabsNames, isVisible)
	{
		///<summary>
		/// Définit la visibilité d'une section
		///</summary>
		///<param name="formContext" type="object">
		/// Contexte du formulaire.
		///</param>
		///<param name="tabsNames" type="Array">
		/// Noms des onglet.
		///</param>
		///<param name="isVisible" type="Boolean">
		/// Indicateur de visibilité.
		///</param>
		for (let tabName of tabsNames)
		{
			var tab = formContext.ui.tabs.get(tabName);
			if (!tab) continue;
			tab.setVisible(isVisible);
		}
	}

	function checkUserSecurityRole(formContext, userId, roleName)
	///<summary>
	/// Vérifie si un utilisateur est configurer avec un rôle de sécurité
	///</summary>
	///<param name="formContext" type="object">
	/// Contexte du formulaire.
	///</param>
	///<param name="userId" type="Guid">
	/// Id du systemuser
	///</param>
	///<param name="roleName" type="Boolean">
	/// Nom du rôle de sécurité
	///</param>
	{
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
			Xrm.WebApi.retrieveMultipleRecords("role", fetchXml).then(

			function success(result)
			{
				if (result.entities.length > 0) successCallback(true);
				else successCallback(false);
			},

			function (error)
			{
				console.log(error.message);
				failureCallback("Erreur");;
			});
		});
	}

	function checkFieldFormat(formContext, regex, field)
	{
		///<summary>
		/// Vérifie le format d'un champ texte
		///</summary>
		///<param name="formContext" type="object">
		/// Contexte du formulaire.
		///</param>
		///<param name="regex" type="Regex">
		/// Regex du format à véérifier.
		///</param>
		///<param name="field" type="String">
		/// Nom du champ à vérifier
		///</param>
		var fieldValue = formContext.getAttribute(field).getValue();
		var notification = {};
		notification.messages = ['Format non conforme'];
		notification.notificationLevel = 'ERROR';
		notification.uniqueId = '0';
		if (!regex.test(fieldValue)) return notification;
		else return null;
	}
    
    //Created on 06/06/25
    function clearFieldOnChange(formContext,fieldToClear, eventType){
        /// Clear a field depending on if the form is loaded or not
        if (eventType==="onChange")
        {
            formContext.getAttribute(fieldToClear).setValue(null);
        }
    }

	return {
		DisableFormSelector: disableFormSelector,
		FilterOptionSet: filterOptionSet,
		GetLookupValue: getLookupValue,
		GetOptionSet: getOptionSet,
		SetAttributesRequiredLevel: setAttributesRequiredLevel,
		SetAttributesVisibility: setAttributesVisibility,
		SetDateToday: setDateToday,
		SetLookupValue: setLookupValue,
		SetLookupValueFromRecord: setLookupValueFromRecord,
		SetControlsDisabled: setControlsDisabled,
		SetControlsVisibility: setControlsVisibility,
		SetSectionVisibility: setSectionVisibility,
		SetState: setState,
		SetTabVisibility: setTabVisibility,
		SetTabsVisibility: setTabsVisibility,
		CheckUserSecurityRole: checkUserSecurityRole,
		CheckFieldFormat: checkFieldFormat,
        ClearFieldOnChange:clearFieldOnChange
	};
}();
