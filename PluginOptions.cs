namespace TraktSuggestionPlugin
{
    using System.ComponentModel;

    using Emby.Web.GenericEdit;
    using Emby.Web.GenericEdit.Validation;

    using MediaBrowser.Model.Attributes;
    using MediaBrowser.Model.GenericEdit;

    public class PluginOptions : EditableOptionsBase
    {
        public override string EditorTitle => "Trakt Suggestion Plugin Settings";

        public override string EditorDescription => "Configure your Trakt credentials below. Provide your Trakt Username, Access Token, and Logging Options.";

        [DisplayName("Trakt Username")]
        [Description("Enter your Trakt username.")]
        public string Username { get; set; }

        [DisplayName("Trakt Access Token")]
        [Description("Enter your Trakt access token.")]
        [MediaBrowser.Model.Attributes.Required]
        public string AccessToken { get; set; }

        [DisplayName("Enable Detailed Logging")]
        [Description("Turn on detailed logging for debugging purposes.")]
        public bool EnableDetailedLogging { get; set; } // New property for detailed logging

        public PluginOptions()
        {
            Username = string.Empty;
            AccessToken = string.Empty;
            EnableDetailedLogging = false; // Default to false
        }
    }
}
