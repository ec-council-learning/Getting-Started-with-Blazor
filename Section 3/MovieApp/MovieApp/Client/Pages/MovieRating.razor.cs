using Microsoft.AspNetCore.Components;

namespace MovieApp.Client.Pages
{
    public class MovieRatingModel : ComponentBase
    {
        [Parameter]
        public decimal? Rating { get; set; }
    }
}
