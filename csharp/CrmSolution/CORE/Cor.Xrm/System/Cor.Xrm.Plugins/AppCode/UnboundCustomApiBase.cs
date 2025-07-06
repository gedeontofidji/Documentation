using Microsoft.Xrm.Sdk;
using System;
using Cor.Xrm.Plugins.AppCode.ServiceProviders;
using Cor.Xrm.Utilities.Extensions;

namespace Cor.Xrm.Plugins.AppCode
{
    public abstract class UnboundCustomApiBase : IPlugin
    {
        public virtual void DefaultMainOperation(UnboundCustomApiServiceProvider ucasp)
        {
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            var esp = new UnboundCustomApiServiceProvider(serviceProvider);

            esp.Trace($"Initiating user:{esp.Context.InitiatingUserId}");
            esp.Trace($"User:{esp.Context.UserId}");

            foreach (var ip in esp.Context.InputParameters)
            {
                if (ip.Value is Entity value)
                {
                    esp.Trace("Input Parameters:");
                    var ipValue = value.ExtractAttributes(null);
                    esp.Trace($"{ip.Key}");
                    esp.Trace(ipValue);
                }
            }

            esp.Trace("");
            esp.Trace("Traitements:");

            try
            {
                DefaultMainOperation(esp);
            }
            catch (Exception error)
            {
                esp.TracingService.Trace(error.ToString());
                throw new InvalidPluginExecutionException(error.Message, error);
            }
        }
    }
}