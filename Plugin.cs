namespace TraktSuggestionPlugin
{
    using MediaBrowser.Common;
    using MediaBrowser.Common.Plugins;
    using MediaBrowser.Model.Logging;
    using MediaBrowser.Controller.Library;
    using MediaBrowser.Controller.Plugins;
    using System;
    using System.IO;
    using System.Reflection;

    public class Plugin : BasePluginSimpleUI<PluginOptions>
    {
        private readonly ILogger _logger;
        private readonly ILibraryManager _libraryManager;

        public Plugin(IApplicationHost applicationHost, ILogManager logManager, ILibraryManager libraryManager)
            : base(applicationHost)
        {
            _logger = logManager.GetLogger(Name);
            _libraryManager = libraryManager;

            // Log plugin load
            _logger.Info("Trakt Suggestion Plugin loaded.");

            // Register the assembly resolve event
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                var assemblyName = new AssemblyName(args.Name).Name;
                string pluginDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string filePath = Path.Combine(pluginDirectory, $"{assemblyName}.dll");

                if (File.Exists(filePath))
                {
                    _logger.Info($"Successfully loaded dependency: {assemblyName}");
                    return Assembly.LoadFrom(filePath);
                }

                _logger.Warn($"Dependency not found: {assemblyName}. Please ensure it exists in the plugins directory.");
                return null;
            };

            // Load your specific dependencies
            LoadDependency("WebSocketSharp-netstandard.dll");
        }

        public override string Name => "Trakt Suggestion Plugin";

        public override string Description => "Fetches Trakt movie recommendations and compares them with your Emby library.";

        public async void FetchAndLogRecommendations()
        {
            var options = GetOptions();
            var traktService = new Services.TraktService(options.AccessToken);
            var embyHelper = new Services.EmbyLibraryHelper(_libraryManager);
            var processor = new Services.SuggestionProcessor(traktService, embyHelper);

            var (inLibrary, notInLibrary) = await processor.GetSuggestionsAsync();

            Log($"Movies in your library: {string.Join(", ", inLibrary)}");
            Log($"Movies not in your library: {string.Join(", ", notInLibrary)}");
        }

        public void Log(string message)
        {
            var options = GetOptions();
            if (options.EnableDetailedLogging)
            {
                _logger.Debug($"[Detailed Log] {message}");
            }
            else
            {
                _logger.Info(message);
            }
        }

        private void LoadDependency(string assemblyName)
        {
            try
            {
                var assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", assemblyName);
                if (File.Exists(assemblyPath))
                {
                    Assembly.LoadFrom(assemblyPath);
                    _logger.Info($"Successfully loaded dependency: {assemblyName}");
                }
                else
                {
                    _logger.Warn($"Dependency not found: {assemblyName}. Please ensure it exists in the plugins directory.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error loading dependency {assemblyName}: {ex.Message}");
            }
        }
    }
}
