using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraktNet;
using TraktNet.Objects.Get.Movies;
using TraktNet.Responses;

namespace TraktSuggestionPlugin
{
    public class TraktApiClient
    {
        private readonly TraktClient _client;

        public TraktApiClient(string clientId, string clientSecret, string accessToken = null)
        {
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                throw new ArgumentNullException("Client ID and Client Secret must be provided.");

            _client = new TraktClient(clientId)
            {
                ClientSecret = clientSecret
            };

            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.Authorization = TraktNet.Objects.Authentication.TraktAuthorization.CreateWith(accessToken);
            }
        }

        // Fetch trending movies from Trakt
        public async Task<IEnumerable<ITraktMovie>> GetTrendingMoviesAsync()
        {
            try
            {
                TraktPagedResponse<ITraktTrendingMovie> response = await _client.Movies.GetTrendingMoviesAsync();

                if (!response.IsSuccess)
                {
                    throw new Exception($"Failed to fetch trending movies: {response.Exception?.Message}");
                }

                // Extract the list of movies
                return response.Value.Select(trendingMovie => trendingMovie.Movie);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching trending movies: {ex.Message}");
            }
        }
    }
}
