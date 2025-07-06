using Microsoft.Xrm.Sdk;
using System;

namespace Cor.Xrm.Plugins.AppCode.ServiceProviders
{
    public class OpportunityCloseServiceProvider<T> : ExtendedServiceProvider<T> where T : Entity
    {
        public OpportunityCloseServiceProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Entity OpportunityClose => GetInputParameter<Entity>(PluginInputParameters.OpportunityClose);
    }
}