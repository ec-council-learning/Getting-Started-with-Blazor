using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MovieApp.Server.Models;
using MovieApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Client.Pages
{
    public class ManageMoviesModel : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        IAuthorizationService AuthService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }

        protected List<Movie> lstMovie = new();
        protected Movie movie = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationState;
            var CheckAdminPolicy = await AuthService
                .AuthorizeAsync(authState.User, Policies.AdminPolicy());

            if (CheckAdminPolicy.Succeeded)
            {
                await GetMovieList();
            }
            else
            {
                NavigationManager.NavigateTo($"login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}");
            }
        }

        protected async Task GetMovieList()
        {
            lstMovie = await Http.GetFromJsonAsync<List<Movie>>("api/Movie");
        }

        protected void DeleteConfirm(int movieID)
        {
            movie = lstMovie.FirstOrDefault(x => x.MovieId == movieID);
        }

        protected async Task DeleteMovie(int movieID)
        {
            await Http.DeleteAsync("api/Movie/" + movieID);
            await GetMovieList();
        }
    }
}
