using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MovieApp.Client.Shared;
using MovieApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Client.Pages
{
    public class AddToWatchlistModel : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        AppStateContainer AppStateContainer { get; set; }

        [Inject]
        IToastService ToastService { get; set; }


        [Parameter]
        public int MovieID { get; set; }

        [Parameter]
        public EventCallback WatchListClick { get; set; }

        [CascadingParameter]
        Task<AuthenticationState> AuthenticationState { get; set; }

        List<Movie> userWatchlist = new();
        protected bool toggle;
        protected string buttonText;

        int UserId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AppStateContainer.OnAppStateChange += StateHasChanged;

            var authState = await AuthenticationState;

            if (authState.User.Identity.IsAuthenticated)
            {
                UserId = Convert.ToInt32(authState.User.FindFirst("userId").Value);
            }
        }

        protected override void OnParametersSet()
        {
            userWatchlist = AppStateContainer.userWatchlist;
            SetWatchlistStatus();
        }

        void SetWatchlistStatus()
        {
            var favouriteMovie = userWatchlist.Find(m => m.MovieId == MovieID);

            if (favouriteMovie != null)
            {
                toggle = true;
            }
            else
            {
                toggle = false;
            }

            SetButtonText();
        }
        void SetButtonText()
        {
            if (toggle)
            {
                buttonText = "Remove from Watchlist";
            }
            else
            {
                buttonText = "Add to Watchlist";
            }
        }

        protected async Task ToggleWatchList()
        {
            if (UserId > 0)
            {
                toggle = !toggle;
                SetButtonText();

                var watchlist =
                    await Http.GetFromJsonAsync<List<Movie>>($"api/Watchlist/ToggleWatchlist/{UserId}/{MovieID}");
                AppStateContainer.SetUserWatchlist(watchlist);

                if (toggle)
                {
                    ToastService.ShowSuccess("Movie added to your Watchlist");
                }
                else
                {
                    ToastService.ShowInfo("Movie removed from your Watchlist");
                }

                await WatchListClick.InvokeAsync();
            }
        }

        public void Dispose()
        {
            AppStateContainer.OnAppStateChange -= StateHasChanged;
        }
    }
}
