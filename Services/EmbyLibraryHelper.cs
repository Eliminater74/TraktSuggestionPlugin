namespace TraktSuggestionPlugin.Services
{
    using System.Collections.Generic;
    using MediaBrowser.Controller.Entities;
    using MediaBrowser.Controller.Entities.Movies;
    using MediaBrowser.Controller.Library;

    public class EmbyLibraryHelper
    {
        private readonly ILibraryManager _libraryManager;

        public EmbyLibraryHelper(ILibraryManager libraryManager)
        {
            _libraryManager = libraryManager;
        }

        public List<string> GetMovieTitlesInLibrary()
        {
            var movies = new List<string>();
            TraverseFolder(_libraryManager.RootFolder, movies);
            return movies;
        }

        private void TraverseFolder(Folder folder, List<string> movies)
        {
            // Create an InternalItemsQuery for fetching children
            var query = new MediaBrowser.Controller.Entities.InternalItemsQuery
            {
                Recursive = false, // Fetch only direct children
                IncludeItemTypes = new[] { "Movie", "Folder" } // Fetch only Movies and Folders
            };

            foreach (var item in _libraryManager.GetItemList(query))
            {
                if (item is Movie movie)
                {
                    movies.Add(movie.Name); // Add movie to the list
                }
                else if (item is Folder subFolder)
                {
                    TraverseFolder(subFolder, movies); // Recursively process subfolders
                }
            }
        }
    }
}
