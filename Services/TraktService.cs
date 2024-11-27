namespace TraktSuggestionPlugin.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TraktNet;
    using TraktNet.Objects.Get.Movies;

    public class TraktService
    {
        private readonly TraktClient _client;

        public TraktService(string accessToken)
        {
            // Pass placeholders for clientId and clientSecret
            _client = new TraktClient("fakeClientId", "fakeClientSecret")
            {
                Authorization = TraktNet.Objects.Authentication.TraktAuthorization.CreateWith(accessToken)
            };
        }

        public async Task<List<ITraktMovie>> GetRecommendationsAsync()
        {
            var response = await _client.Movies.GetTrendingMoviesAsync();

            if (!response.IsSuccess)
            {
                throw new System.Exception("Failed to fetch recommendations from Trakt. Please check your Access Token and try again.");
            }

            return response.Value?.OfType<ITraktMovie>().ToList() ?? new List<ITraktMovie>();
        }
    }
}
