using Microsoft.AspNetCore.Components;
using MovieApp.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Client.Pages
{
    public class HomeModel : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        [Parameter]
        public string GenreName { get; set; }

        protected List<Movie> lstMovie = new();
        protected List<Movie> filteredMovie = new();

        protected override async Task OnInitializedAsync()
        {
            await GetMovieList();
        }
        async Task GetMovieList()
        {
            lstMovie = await Http.GetFromJsonAsync<List<Movie>>("api/Movie");
            filteredMovie = lstMovie;
        }

        protected override void OnParametersSet()
        {
            FilterMovie();
        }
        void FilterMovie()
        {
            if (!string.IsNullOrEmpty(GenreName))
            {
                lstMovie = filteredMovie.Where(m => m.Genre == GenreName).ToList();
            }
            else
            {
                lstMovie = filteredMovie;
            }
        }
    }
}
