using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraktNet;
using TraktNet.Objects.Get.Movies;

namespace TraktSuggestionPlugin
{
    public class TraktApiClient
    {
        private readonly TraktClient _client;

        public TraktApiClient(string accessToken)
        {
            // Provide a dummy Client ID; it won't be used with an Access Token only
            _client = new TraktClient("dummy-client-id")
            {
                Authorization = TraktNet.Objects.Authentication.TraktAuthorization.CreateWith(accessToken)
            };
        }

        public async Task<IEnumerable<ITraktMovie>> GetTrendingMoviesAsync()
        {
            var response = await _client.Movies.GetTrendingMoviesAsync();
            if (!response.IsSuccess)
            {
                throw new Exception($"Failed to fetch trending movies: {response.Exception?.Message}");
            }
            return response.Value.Select(trendingMovie => trendingMovie.Movie);
        }
    }

}
