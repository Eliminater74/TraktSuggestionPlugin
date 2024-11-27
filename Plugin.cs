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
        private readonly Guid id = new Guid("11111111-2222-3333-4444-555555555555");
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
            this.logger.Info("Trakt Suggestion Plugin options have been updated: Username={0}, AccessToken={1}, DetailedLogging={2}",
                options.Username,
                options.AccessToken,
                options.EnableDetailedLogging);
        }

        public void Log(string message)
        {
            var options = GetOptions(); // Access configuration using GetOptions()
            if (options.EnableDetailedLogging)
            {
                logger.Debug($"[Detailed Log] {message}");
            }
            else
            {
                logger.Info(message);
            }
        }
    }
}
