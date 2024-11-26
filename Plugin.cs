namespace TraktSuggestionPlugin
{
    using System;
    using System.IO;

    using MediaBrowser.Common;
    using MediaBrowser.Common.Plugins;
    using MediaBrowser.Controller.Plugins;
    using MediaBrowser.Model.Drawing;
    using MediaBrowser.Model.Logging;

    /// <summary>
    /// The Trakt Suggestion Plugin.
    /// </summary>
    public class Plugin : BasePluginSimpleUI<PluginOptions>, IHasThumbImage
    {
        /// <summary>The Plugin ID.</summary>
        private readonly Guid id = new Guid("06B96E58-B7AF-4604-9E0B-048DBE66C655"); // Replace with a generated GUID

        private readonly ILogger logger;

        /// <summary>Initializes a new instance of the <see cref="Plugin" /> class.</summary>
        /// <param name="applicationHost">The application host.</param>
        /// <param name="logManager">The log manager.</param>
        public Plugin(IApplicationHost applicationHost, ILogManager logManager) : base(applicationHost)
        {
            this.logger = logManager.GetLogger(this.Name);
            this.logger.Info("Trakt Suggestion Plugin ({0}) is getting loaded", this.Name);
        }

        /// <summary>Gets the description.</summary>
        /// <value>The description.</value>
        public override string Description => "Provides movie suggestions from Trakt and compares them with your Emby library.";

        /// <summary>Gets the unique id.</summary>
        /// <value>The unique id.</value>
        public override Guid Id => this.id;

        /// <summary>Gets the name of the plugin.</summary>
        /// <value>The name.</value>
        public override sealed string Name => "Trakt Suggestion Plugin";

        /// <summary>Gets the thumb image format.</summary>
        /// <value>The thumb image format.</value>
        public ImageFormat ThumbImageFormat => ImageFormat.Png;

        /// <summary>Gets the thumb image.</summary>
        /// <returns>An image stream.</returns>
        public Stream GetThumbImage()
        {
            var type = this.GetType();
            return type.Assembly.GetManifestResourceStream(type.Namespace + ".ThumbImage.png");
        }

        /// <summary>
        /// Triggered when plugin options are saved.
        /// </summary>
        /// <param name="options">The updated plugin options.</param>
        protected override void OnOptionsSaved(PluginOptions options)
        {
            this.logger.Info("Trakt Suggestion Plugin options have been updated.");
            base.OnOptionsSaved(options);

            // Perform any additional actions when options are updated
        }
    }
}
