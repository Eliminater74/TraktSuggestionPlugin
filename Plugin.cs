namespace TraktSuggestionPlugin
{
    using System;
    using System.IO;

    using MediaBrowser.Common;
    using MediaBrowser.Common.Plugins;
    using MediaBrowser.Controller.Plugins;
    using MediaBrowser.Model.Drawing;
    using MediaBrowser.Model.Logging;

    public class Plugin : BasePluginSimpleUI<PluginOptions>, IHasThumbImage
    {
        private readonly Guid id = new Guid("90011765-13A5-4699-91E1-830AAC746FAB"); // Replace with your GUID
        private readonly ILogger logger;

        public Plugin(IApplicationHost applicationHost, ILogManager logManager) : base(applicationHost)
        {
            this.logger = logManager.GetLogger(this.Name);
            this.logger.Info("Trakt Suggestion Plugin ({0}) is getting loaded", this.Name);
        }

        public override string Description => "Provides personalized movie suggestions from Trakt.";

        public override Guid Id => this.id;

        public override string Name => "Trakt Suggestion Plugin";

        public ImageFormat ThumbImageFormat => ImageFormat.Png;

        public Stream GetThumbImage()
        {
            var type = this.GetType();
            return type.Assembly.GetManifestResourceStream(type.Namespace + ".ThumbImage.png");
        }

        protected override void OnOptionsSaved(PluginOptions options)
        {
            this.logger.Info("Trakt Suggestion Plugin options have been updated: Username={0}, AccessToken={1}", options.Username, options.AccessToken);
        }
    }
}
