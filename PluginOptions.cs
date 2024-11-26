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

        public override string EditorDescription => "Configure your Trakt credentials below. Provide your Trakt Username and Access Token.";

        [DisplayName("Trakt Username")]
        [Description("Enter your Trakt username.")]
        public string Username { get; set; }

        [DisplayName("Trakt Access Token")]
        [Description("Enter your Trakt access token.")]
        [MediaBrowser.Model.Attributes.Required]
        public string AccessToken { get; set; }

        public PluginOptions()
        {
            Username = string.Empty;
            AccessToken = string.Empty;
        }

        protected override void Validate(ValidationContext context)
        {
            if (string.IsNullOrEmpty(AccessToken))
            {
                context.AddValidationError(nameof(AccessToken), "Access Token is required.");
            }
        }
    }
}
