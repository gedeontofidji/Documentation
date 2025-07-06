using Microsoft.Xrm.Sdk;
using System;

namespace Cor.Xrm.Plugins.AppCode.ServiceProviders
{
    public class UnboundCustomApiServiceProvider : ExtendedServiceProvider<Entity>
    {
        public UnboundCustomApiServiceProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}