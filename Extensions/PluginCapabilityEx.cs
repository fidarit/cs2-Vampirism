using CounterStrikeSharp.API.Core.Capabilities;
using System;

namespace VampirismCS2.Extensions
{
    internal static class PluginCapabilityEx
    {
        /// <returns>Null if plugin is not registered yet</returns>
        public static T GetPluginOrNull<T>(this PluginCapability<T> capability)
        {
			try
			{
                return capability.Get();
			}
			catch (Exception)
			{
                return default;
			}
        }
    }
}
