function CreateOpportunityPlugin
{
    param (
        [string]$ProjectPath,
        [string]$ProjectName,
        [string]$EditorName
    )

    $pluginFilePath = Join-Path $ProjectPath "OpportunityPlugin.cs"

    # Code template de base pour un plugin Dynamics
    $pluginCode = @"
using $EditorName.Xrm.EntityWrappers;
using $EditorName.Xrm.Plugins.AppCode;
using $EditorName.Xrm.Plugins.AppCode.Attributes;
using $EditorName.Xrm.Plugins.AppCode.ServiceProviders;
using $EditorName.Xrm.Service;
using Microsoft.Xrm.Sdk;

namespace $ProjectName
{
    [PrimaryEntity(Opportunity.EntityLogicalName)]
    public class OpportunityPlugin : Plugin<Opportunity>, IPlugin
    {
        /// <summary>
        /// Le constructeur est nécessaire pour passer les configurations
        /// de fonctionnalité
        /// </summary>
        /// <remarks>
        /// La configuration de fonctionnalité doit être définie dans la
        /// configuration non sécurisée de l'étape de traitement de plugin
        /// comme ci-dessous:
        /// {"Features":[{"Enabled":true,"Name":"MaFonctionnalité"},{"Enabled":false,"Name":"feature2"}]}
        /// </remarks>
        /// <param name="unsecureConfiguration"></param>
        /// <param name="secureConfiguration"></param>
        public OpportunityPlugin(string unsecureConfiguration, string secureConfiguration)
        : base(unsecureConfiguration, secureConfiguration)
        {
        }

        [Description("Evénement Post Create sur l'opportunité")]
        [Configuration("")]
        [SecureConfiguration("")]
        [SecondaryEntity("none")]
        [Mode(PluginMode.Synchronous)]
        [FilteringAttributes("")]
        [InvocationSource(InvocationSource.Parent)]
        [SupportedDeployment(SupportedDeployment.ServerOnly)]
        [Rank(1)]
        public override void PostOperationCreate(CreationServiceProvider<Opportunity> csp)
        {
            var opportunity = csp.Target;
            var opportunityService = new OpportunityService(csp);
        }

        [Description("Evénement Post Win sur l'opportunité")]
        [Configuration("")]
        [SecureConfiguration("")]
        [SecondaryEntity("none")]
        [Mode(PluginMode.Synchronous)]
        [FilteringAttributes("")]
        [InvocationSource(InvocationSource.Parent)]
        [SupportedDeployment(SupportedDeployment.ServerOnly)]
        [Image("Image", PluginImageType.Post, "")]
        [Rank(1)]
        public override void PostOperationWin(OpportunityCloseServiceProvider<Opportunity> esp)
        {
            var OpportunityClose = esp.OpportunityClose.ToEntity<OpportunityClose>();
            OpportunityService opportunityService = new OpportunityService(esp);
        }

        [Description("Evénement Post Lose sur l'opportunité")]
        [Configuration("")]
        [SecureConfiguration("")]
        [SecondaryEntity("none")]
        [Mode(PluginMode.Synchronous)]
        [FilteringAttributes("")]
        [InvocationSource(InvocationSource.Parent)]
        [SupportedDeployment(SupportedDeployment.ServerOnly)]
        [Image("Image", PluginImageType.Post, "")]
        [Rank(1)]
        public override void PostOperationLose(OpportunityCloseServiceProvider<Opportunity> esp)
        {
            var OpportunityClose = esp.OpportunityClose.ToEntity<OpportunityClose>();
            OpportunityService opportunityService = new OpportunityService(esp);
        }

        [Description("Evénement Pre Create sur l'opportunité")]
        [Configuration("")]
        [SecureConfiguration("")]
        [SecondaryEntity("none")]
        [Mode(PluginMode.Synchronous)]
        [FilteringAttributes("")]
        [InvocationSource(InvocationSource.Parent)]
        [SupportedDeployment(SupportedDeployment.ServerOnly)]
        [Rank(1)]
        public override void PreOperationCreate(CreationServiceProvider<Opportunity> csp)
        {
            var opportunity = csp.Target;
        }

        [Description("Evénement Post Update sur l'opportunité")]
        [Configuration("")]
        [SecureConfiguration("")]
        [SecondaryEntity("none")]
        [Mode(PluginMode.Synchronous)]
        [FilteringAttributes("")]
        [InvocationSource(InvocationSource.Parent)]
        [SupportedDeployment(SupportedDeployment.ServerOnly)]
        [Image("Image", PluginImageType.Pre, "cve_opportuniteparenteid")]
        [Rank(1)]
        public override void PostOperationUpdate(UpdateServiceProvider<Opportunity> usp)
        {
            var preData = usp.GetPreImage<Opportunity>("Image");
            var service = new OpportunityService(usp);
        }
    }
}
"@   

        New-Item -ItemType File -Path $pluginFilePath | Out-Null
        Set-Content -Path $pluginFilePath -Value $pluginCode

        Write-Host "Plugin class '$ClassName.cs' created in '$ProjectPath'" -ForegroundColor Cyan
        Write-Host
}