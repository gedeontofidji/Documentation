using Microsoft.Xrm.Sdk;
using System;
using System.Linq;

namespace Cor.Xrm.Plugins.AppCode.ServiceProviders
{
    public class QualifyServiceProvider<T> : ExtendedServiceProvider<T> where T : Entity
    {
        public QualifyServiceProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public bool CreateAccount => GetInputParameter<bool>(PluginInputParameters.CreateAccount);
        public bool CreateContact => GetInputParameter<bool>(PluginInputParameters.CreateContact);

        public EntityReference CreatedAccount =>
            GetOutputParameter<EntityReferenceCollection>(PluginOutputParameters.CreatedEntities)
                .FirstOrDefault(e => e.LogicalName == "account");

        public EntityReference CreatedContact =>
            GetOutputParameter<EntityReferenceCollection>(PluginOutputParameters.CreatedEntities)
                .FirstOrDefault(e => e.LogicalName == "contact");

        public EntityReference CreatedOpportunity =>
            GetOutputParameter<EntityReferenceCollection>(PluginOutputParameters.CreatedEntities)
                .FirstOrDefault(e => e.LogicalName == "opportunity");

        public bool CreateOpportunity => GetInputParameter<bool>(PluginInputParameters.CreateOpportunity);
        public EntityReference LeadId => GetInputParameter<EntityReference>(PluginInputParameters.LeadId);
        public EntityReference OpportunityCurrencyId => GetInputParameter<EntityReference>(PluginInputParameters.OpportunityCurrencyId);
        public EntityReference OpportunityCustomerId => GetInputParameter<EntityReference>(PluginInputParameters.OpportunityCustomerId);
        public EntityReference ProcessInstanceId => GetInputParameter<EntityReference>(PluginInputParameters.ProcessInstanceId);
        public EntityReference SourceCampaignId => GetInputParameter<EntityReference>(PluginInputParameters.SourceCampaignId);
        public OptionSetValue Status => GetInputParameter<OptionSetValue>(PluginInputParameters.Status);
    }
}