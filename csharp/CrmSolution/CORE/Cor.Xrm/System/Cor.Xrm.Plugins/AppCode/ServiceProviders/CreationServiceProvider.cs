using Microsoft.Xrm.Sdk;
using System;

namespace Cor.Xrm.Plugins.AppCode.ServiceProviders
{
    public class CreationServiceProvider<T> : ExtendedServiceProvider<T> where T : Entity
    {
        public CreationServiceProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Guid Id => GetOutputParameter<Guid>(PluginOutputParameters.Id);
        public T Target => GetInputData().ToEntity<T>();
        public Entity TargetEntity => GetInputData();
    }
}