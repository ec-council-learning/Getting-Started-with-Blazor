using Microsoft.AspNetCore.Components;
using MovieApp.Server.Models;

namespace MovieApp.Client.Pages
{
    public class MovieCardModel : ComponentBase
    {
        [Parameter]
        public Movie Movie { get; set; }

        protected string imagePreview;

        protected override void OnParametersSet()
        {
            imagePreview = "/Poster/" + Movie.PosterPath;
        }
    }
}
