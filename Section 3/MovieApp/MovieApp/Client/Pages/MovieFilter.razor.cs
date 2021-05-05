using Microsoft.AspNetCore.Components;
using MovieApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Client.Pages
{
    public class MovieFilterModel : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string SelectedGenre { get; set; }

        protected List<Genre> lstGenre = new();

        protected override async Task OnInitializedAsync()
        {
            await GetGenreList();
        }

        protected async Task GetGenreList()
        {
            lstGenre = await Http.GetFromJsonAsync<List<Genre>>("api/Movie/GetGenreList");
        }

        protected void SelectGenre(string genreName)
        {
            if (string.IsNullOrEmpty(genreName))
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                NavigationManager.NavigateTo("/category/" + genreName);
            }
        }
    }
}
