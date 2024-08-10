using VampirismCS2.Models;
using Microsoft.Extensions.Logging;

namespace VampirismCS2.Controllers
{
    internal abstract class BaseController
    {
        public Plugin Plugin { get; }

        public PluginConfig Config => Plugin.Config;
        public ILogger Logger => Plugin.Logger;

        protected BaseController(Plugin plugin)
        {
            Plugin = plugin;
        }
    }
}
