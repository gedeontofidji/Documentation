using System;

namespace Cor.Xrm.Plugins.AppCode.Exceptions
{
    public class PluginImageNotFoundException : Exception
    {
        public PluginImageNotFoundException(string message) : base(message)
        {
        }
    }
}