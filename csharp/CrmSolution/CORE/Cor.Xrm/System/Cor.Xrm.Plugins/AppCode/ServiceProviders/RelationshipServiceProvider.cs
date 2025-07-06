using Microsoft.Xrm.Sdk;
using System;

namespace Cor.Xrm.Plugins.AppCode.ServiceProviders
{
    public class RelationshipServiceProvider<T> : ExtendedServiceProvider<T> where T : Entity
    {
        public RelationshipServiceProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public EntityReferenceCollection RelatedEntities => GetInputParameter<EntityReferenceCollection>(PluginInputParameters.RelatedEntities);
        public Relationship RelationShip => GetInputParameter<Relationship>(PluginInputParameters.Relationship);
        public EntityReference Target => GetInputParameter<EntityReference>(PluginInputParameters.Target);
    }
}