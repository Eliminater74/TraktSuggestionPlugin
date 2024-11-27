namespace TraktSuggestionPlugin
{
    using System.ComponentModel;

    using Emby.Web.GenericEdit;
    using MediaBrowser.Model.Attributes;

    public class PluginOptions : EditableOptionsBase
    {
        public override string EditorTitle => "Trakt Suggestion Plugin Settings";

        public override string EditorDescription => "Provide your Trakt credentials below and configure logging options.";

        [DisplayName("Trakt Username")]
        [Description("Enter your Trakt username.")]
        public string Username { get; set; }

        [DisplayName("Trakt Access Token")]
        [Description("Enter your Trakt access token.")]
        [Required]
        public string AccessToken { get; set; }

        [DisplayName("Enable Detailed Logging")]
        [Description("Enable detailed logging for debugging purposes.")]
        public bool EnableDetailedLogging { get; set; } // Added detailed logging option

        public PluginOptions()
        {
            Username = string.Empty;
            AccessToken = string.Empty;
            EnableDetailedLogging = false; // Default to false
        }
    }
}
