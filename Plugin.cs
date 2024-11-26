using MediaBrowser.Common;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Serialization;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Common.Configuration;
using System.Collections.Generic;

namespace TraktSuggestionPlugin
{
    public class Plugin : BasePlugin<PluginOptions>, IHasWebPages
    {
        private readonly ILogger _logger;

        // Static instance of the plugin
        public static Plugin Instance { get; private set; }

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer, ILogManager logManager)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this; // Assign the current instance to the static property
            _logger = logManager.GetLogger(Name);
            _logger.Info("Trakt Suggestion Plugin has been loaded.");
        }

        public override string Name => "Trakt Suggestion Plugin";

        public override string Description => "Provides personalized movie suggestions from Trakt for each user.";

        // Register the settings page
        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "TraktSuggestions",
                    EmbeddedResourcePath = $"{GetType().Namespace}.Configuration.TraktSuggestions.html",
                    EnableInMainMenu = false // Only available in the plugin settings
                }
            };
        }
    }
}
