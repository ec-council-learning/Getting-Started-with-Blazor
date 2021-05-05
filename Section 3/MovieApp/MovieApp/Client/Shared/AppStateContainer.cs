using MovieApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Client.Shared
{
    public class AppStateContainer
    {
        private readonly HttpClient _httpClient;

        public AppStateContainer(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Movie> userWatchlist = new();

        public event Action OnAppStateChange;

        public async Task GetUserWatchlist(int userId)
        {
            var currentUserWatchlist = await _httpClient.GetFromJsonAsync<List<Movie>>("api/Watchlist/" + userId);
            SetUserWatchlist(currentUserWatchlist);
        }

        public void SetUserWatchlist(List<Movie> lstMovie)
        {
            userWatchlist = lstMovie;
            NotifyAppStateChanged();
        }

        private void NotifyAppStateChanged() => OnAppStateChange?.Invoke();
    }
}
