using MediaBrowser.Model.Plugins;

namespace TraktSuggestionPlugin
{
    public class TraktPluginConfiguration : BasePluginConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }

        public TraktPluginConfiguration()
        {
            ClientId = string.Empty;
            ClientSecret = string.Empty;
            AccessToken = string.Empty;
        }
    }
}
