using System;

namespace Cor.Xrm.Plugins.AppCode.Exceptions
{
    public class PluginParameterNotFoundException : Exception
    {
        public PluginParameterNotFoundException(string message) : base(message)
        {
        }
    }
}