using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MovieApp.Client.Shared;
using MovieApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Client.Pages
{
    public class WatchlistModel : ComponentBase
    {
        [Inject]
        AppStateContainer AppStateContainer { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        Task<AuthenticationState> AuthenticationState { get; set; }

        protected List<Movie> watchlist = new();

        protected override async Task OnInitializedAsync()
        {
            AppStateContainer.OnAppStateChange += StateHasChanged;

            var authState = await AuthenticationState;

            if (authState.User.Identity.IsAuthenticated)
            {
                GetUserWatchlist();
            }
            else
            {
                NavigationManager.NavigateTo($"login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}");
            }
        }
        protected void WatchListClickHandler()
        {
            GetUserWatchlist();
        }

        void GetUserWatchlist()
        {
            watchlist = AppStateContainer.userWatchlist;
        }

        public void Dispose()
        {
            AppStateContainer.OnAppStateChange -= StateHasChanged;
        }
    }
}
