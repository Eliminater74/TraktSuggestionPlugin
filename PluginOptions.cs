namespace TraktSuggestionPlugin
{
    using System.ComponentModel;
    using Emby.Web.GenericEdit;
    using Emby.Web.GenericEdit.Validation;

    public class PluginOptions : EditableOptionsBase
    {
        public override string EditorTitle => "Trakt Suggestion Plugin Options";

        public override string EditorDescription => "Set your Trakt Access Token to fetch personalized movie suggestions.";

        [DisplayName("Trakt Access Token")]
        [Description("Enter your Trakt API Access Token. This is required for the plugin to work.")]
        [MediaBrowser.Model.Attributes.Required]
        public string AccessToken { get; set; }

        protected override void Validate(ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(AccessToken))
            {
                context.AddValidationError(nameof(AccessToken), "Access Token is required.");
            }
        }
    }
}
