using Microsoft.AspNetCore.Components;
using MovieApp.Server.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Client.Pages
{
    public class MovieDetailsModel : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        [Parameter]
        public int MovieID { get; set; }

        public Movie movie = new();
        protected string imagePreview;
        protected string movieDuration;

        protected override async Task OnParametersSetAsync()
        {
            movie = await Http.GetFromJsonAsync<Movie>("api/Movie/" + MovieID);
            ConvertMinToHour();
            imagePreview = "/Poster/" + movie.PosterPath;
        }

        void ConvertMinToHour()
        {
            TimeSpan movieLength = TimeSpan.FromMinutes(movie.Duration);
            movieDuration = string.Format("{0:0}h {1:00}min", (int)movieLength.TotalHours, movieLength.Minutes);
        }
    }
}
