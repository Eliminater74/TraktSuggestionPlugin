namespace TraktSuggestionPlugin.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SuggestionProcessor
    {
        private readonly TraktService _traktService;
        private readonly EmbyLibraryHelper _libraryHelper;

        public SuggestionProcessor(TraktService traktService, EmbyLibraryHelper libraryHelper)
        {
            _traktService = traktService;
            _libraryHelper = libraryHelper;
        }

        public async Task<(List<string> InLibrary, List<string> NotInLibrary)> GetSuggestionsAsync()
        {
            var traktRecommendations = await _traktService.GetRecommendationsAsync();
            var embyMovies = _libraryHelper.GetMovieTitlesInLibrary();

            var inLibrary = traktRecommendations
                .Where(movie => embyMovies.Contains(movie.Title))
                .Select(movie => movie.Title)
                .ToList();

            var notInLibrary = traktRecommendations
                .Where(movie => !embyMovies.Contains(movie.Title))
                .Select(movie => movie.Title)
                .ToList();

            return (inLibrary, notInLibrary);
        }
    }
}
