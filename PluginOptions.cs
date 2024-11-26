namespace TraktSuggestionPlugin
{
    using System.ComponentModel;

    using Emby.Web.GenericEdit;
    using Emby.Web.GenericEdit.Validation;

    using MediaBrowser.Model.Attributes;
    using MediaBrowser.Model.GenericEdit;

    public class PluginOptions : EditableOptionsBase
    {
        public override string EditorTitle => "Trakt Suggestion Plugin Options";

        public override string EditorDescription => "Configure your Trakt API credentials and preferences.";

        [DisplayName("Trakt Client ID")]
        [Description("Enter your Trakt API Client ID. This value is required.")]
        [MediaBrowser.Model.Attributes.Required]
        public string ClientId { get; set; }

        [DisplayName("Trakt Client Secret")]
        [Description("Enter your Trakt API Client Secret. This value is required.")]
        [MediaBrowser.Model.Attributes.Required]
        public string ClientSecret { get; set; }

        [DisplayName("Trakt Access Token")]
        [Description("Enter your Trakt API Access Token (optional).")]
        public string AccessToken { get; set; }

        [DisplayName("Trakt Username")]
        [Description("Enter your Trakt username. This value is required.")]
        [MediaBrowser.Model.Attributes.Required]
        public string Username { get; set; }

        protected override void Validate(ValidationContext context)
        {
            // Validate Client ID
            if (string.IsNullOrWhiteSpace(ClientId))
            {
                context.AddValidationError(nameof(ClientId), "Client ID is required.");
            }

            // Validate Client Secret
            if (string.IsNullOrWhiteSpace(ClientSecret))
            {
                context.AddValidationError(nameof(ClientSecret), "Client Secret is required.");
            }

            // Validate Username
            if (string.IsNullOrWhiteSpace(Username))
            {
                context.AddValidationError(nameof(Username), "Username is required.");
            }

            // Validate optional fields
            if (AccessToken?.Length > 0 && AccessToken.Length < 10)
            {
                context.AddValidationError(nameof(AccessToken), "Access Token must be at least 10 characters if provided.");
            }
        }
    }
}
