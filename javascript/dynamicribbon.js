var Cto = Cto || {};
Cto.Ribbon = Cto.Ribbon || {};
Cto.Ribbon.entityName = function ()
{
	var formContext;
    	var populationXML = "";

    	function populateRibbonMenu(commandProperties, context)
	{
		var userSettings = Xrm.Utility.getGlobalContext().userSettings;
		var fetchData = { systemuserid: userSettings.userId };
		var fetchXml = [
	        "<fetch distinct='true'>",
	        "  <entity name='cto_entityName'>",
	        "    <attribute name='cto_field1' />",
	        "    <attribute name='cto_field2' />",
	        "    <attribute name='cto_field3' />", //fields to retrieve
	        "    <filter>",
	        "    	<condition attribute='cto_entityName' operator='eq' value='1' />",
	        "    </filter>",
	        "    <link-entity name='cto_entityName_team' from='cto_entityNameid' to='cto_entityNameid' alias='t' intersect='true'>",
	        "      <link-entity name='teammembership' from='teamid' to='teamid' alias='tm' intersect='true'>",
	        "        <filter>",
	        "          <condition attribute='systemuserid' operator='eq' value='", fetchData.systemuserid, "'/>",
	        "        </filter>",
	        "      </link-entity>",
	        "    </link-entity>",
	        "  </entity>",
	        "</fetch>",
	        ].join("");
		var result = executeFetchXml("cto_entityPluralName", fetchXml)
        
		var command = "cto.entityName.ClickRibbonMenu"; //Id in Ribbon Workbench of the click command
		//This code is used to build the command string for UCI
		if (commandProperties.SourceControlId != null)
		{
			var source = commandProperties.SourceControlId.split('|');
			if (source.length > 3)
			{
				//command="entity|NoRelationship|Form|Command"
				command = source[0] + "|" + source[1] + "|" + source[2] + "|" + command;
			}
		}
        
		var menuRibbonXml = "<MenuSection Id='cto.entityName.SelectTemplate.MenuSection' Sequence='10'><Controls Id='cto.entityName.SelectTemplate.Control'>";
		for (var i = 0; i < result.value.length; i++)
		{
			var Name = result.value[i].cto_field1; //Name that will be displayed on the button items
            		var Value = result.value[i].cto_field2 + "|" + result.value[i].cto_field3; //Id of each item that can be used in the populateRibbonClick to know which item was triggered. Do not use url or anything with special characters.
			menuRibbonXml += "<Button Id='" + Value + "' Command='" + command + "'  Sequence='" + ((i + 1) * 10) + "' LabelText='" + Name + "' />"
		}
		menuRibbonXml += "</Controls></MenuSection>";
		commandProperties["PopulationXML"] = '<Menu Id="cto.entityName.SelectRibbon.Menu">' + menuRibbonXml + "</Menu>";
	}
    
    	function populateRibbonClick(commandProperties, context)
	{        
        var entityId = context.data.entity.getId().replace(/[{}]/g, "").toLowerCase(); // Get the current ID record
	var itemId = commandProperties.SourceControlId; //Id of the item triggered
	var itemsId = itemId.split('|');
	var field1 = itemsId[0]; //Get the first field of the ID
        //Define here the actions you want to execute when a user clicks an item.
	}
    
    	function executeFetchXml(entityPluralName, fetchXml)
	{
		var data = null;
		var req = new XMLHttpRequest();
		req.open('GET', Xrm.Page.context.getClientUrl() + "/api/data/v9.2/" + entityPluralName + "?fetchXml=" + encodeURIComponent(fetchXml), false);
		req.setRequestHeader("Accept", "application/json");
		req.setRequestHeader("Content-type", "application/json; charset=utf-8");
		req.setRequestHeader("OData-MaxVersion", "4.0");
		req.setRequestHeader("OData-Version", "4.0");
		req.send();
		if (req.readyState === 4)
		{
			if (req.status === 200)
			{
				data = JSON.parse(req.response);
			}
			else
			{
				var error = JSON.parse(req.response).error;
				console.log("Oops, something went wrong:" + error.message);
			}
		}
		return data;
	}

	return {
        PopulateRibbonMenu: populateRibbonMenu,
        PopulateRibbonClick: populateRibbonClick,
	};
}();
