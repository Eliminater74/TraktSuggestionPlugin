using MediaBrowser.Model.Plugins;

namespace TraktSuggestionPlugin
{
    public class PluginOptions : BasePluginConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }
        public string Username { get; set; }

        public PluginOptions()
        {
            ClientId = string.Empty;
            ClientSecret = string.Empty;
            AccessToken = string.Empty;
            Username = string.Empty;
        }
    }
}
