function CreateOpportunityService
{
    param (
        [string]$ProjectPath,
        [string]$ProjectName,
        [string]$EditorName
    )

    $pluginFilePath = Join-Path $ProjectPath "OpportunityService.cs"

    # Code template de base pour un plugin Dynamics
    $pluginCode = @"
using $EditorName.Xrm.EntityWrappers;
using $EditorName.Xrm.Service.Base;
using $EditorName.Xrm.Utilities.Extensions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MscrmTools.FluentQueryExpressions;
using System;
using System.Collections.Generic;

namespace $ProjectName
{
    public class OpportunityService : ServiceBase
    {
        #region constructeur

        public OpportunityService(IOrganizationService adminService,
            IOrganizationService userService,
            ITracingService tracingService = null)
            : base(adminService, userService, tracingService)
        {
        }

        public OpportunityService(IDataverseServiceProvider provider) : base(provider)
        {
        }

        #endregion constructeur

        /// <summary>
        /// Génere des enregistrements Fichier d'Opportunité pour l'opportunité
        /// courante
        /// </summary>
        /// <param name="opportunity">Données de l'opportunité</param>
        public void GenerateFilesRecord(Opportunity opportunity)
        {
            TraceMethodStart();
            var query = new Query<$editorname.tolower()_TypeFichierOpportunite>()
                     .NoLock()
                     .Select($editorname.tolower()_TypeFichierOpportunite.Fields.$editorname.tolower()_Nom, $editorname.tolower()_TypeFichierOpportunite.Fields.$editorname.tolower()_TypeDocument);

            if (opportunity.$editorname.tolower()_BusinessUnit != null)
                query.WhereEqual($editorname.tolower()_TypeFichierOpportunite.Fields.$editorname.tolower()_BusinessUnit, opportunity.$editorname.tolower()_BusinessUnit.Id);
            else
            {
                TraceMethodEnd("Pas de Division sur l'opportunité, on sort");
                return;
            }

            if (opportunity.$editorname.tolower()_TypeProjet != null)
            {
                query.WhereContainValues($editorname.tolower()_TypeFichierOpportunite.Fields.$editorname.tolower()_TypeOpportuniteCode, opportunity.$editorname.tolower()_TypeProjet.Value);
            }

            if (opportunity.$editorname.tolower()_OpportuniteParenteID != null)
            {
                query.WhereEqual($editorname.tolower()_TypeFichierOpportunite.Fields.$editorname.tolower()_EstValidePourOpportuniteFille, true);
            }
            else
            {
                query.WhereEqual($editorname.tolower()_TypeFichierOpportunite.Fields.$editorname.tolower()_EstValidePourOpportunite, true);
            }

            var filesDefs = query.GetAll(AdminService);

            Trace($"{filesDefs.Count} fichiers à générer");

            foreach (var fileDef in filesDefs)
            {
                Trace($"Génération du fichier {fileDef.$editorname.tolower()_Nom}");
                Trace($"Type de document {fileDef.$editorname.tolower()_TypeDocument}");
                var record = new $editorname.tolower()_FichierOpportunite
                {
                    $editorname.tolower()_Nom = fileDef.$editorname.tolower()_Nom,
                    $editorname.tolower()_TypeDocument = fileDef.$editorname.tolower()_TypeDocument,
                    $editorname.tolower()_RegardingObjectId = opportunity.ToEntityReference(),
                    StatusCode = new OptionSetValue(1), // Fichier à télécharger
                    OwnerId = opportunity.OwnerId
                };

                AdminService.Create(record);
            }

            TraceMethodEnd();
        }
    }
}
"@   

        New-Item -ItemType File -Path $pluginFilePath | Out-Null
        Set-Content -Path $pluginFilePath -Value $pluginCode

        Write-Host "Plugin class '$ClassName.cs' created in '$ProjectPath'" -ForegroundColor Yellow
        Write-Host
}