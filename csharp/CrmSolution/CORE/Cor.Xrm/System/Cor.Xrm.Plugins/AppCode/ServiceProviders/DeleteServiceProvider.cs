using Microsoft.Xrm.Sdk;
using System;

namespace Cor.Xrm.Plugins.AppCode.ServiceProviders
{
    public class DeleteServiceProvider<T> : ExtendedServiceProvider<T> where T : Entity
    {
        public DeleteServiceProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public EntityReference Target => GetInputParameter<EntityReference>(PluginInputParameters.Target);
    }
}