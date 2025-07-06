using Microsoft.Xrm.Sdk;
using System;
using Cor.Xrm.Utilities.Extensions;

namespace Cor.Xrm.Plugins.AppCode.ServiceProviders
{
    public class BoundCustomApiServiceProvider<T> : ExtendedServiceProvider<T> where T : Entity
    {
        public BoundCustomApiServiceProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public T Target => base.GetInputParameter<EntityReference>(PluginInputParameters.Target).ToEntity<T>();
    }
}