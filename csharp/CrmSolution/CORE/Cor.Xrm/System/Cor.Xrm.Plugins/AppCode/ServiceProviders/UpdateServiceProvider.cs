using Microsoft.Xrm.Sdk;
using System;

namespace Cor.Xrm.Plugins.AppCode.ServiceProviders
{
    public class UpdateServiceProvider<T> : ExtendedServiceProvider<T> where T : Entity
    {
        public UpdateServiceProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public T Target => GetInputData().ToEntity<T>();

        public Entity TargetEntity => GetInputData();
    }
}