using System.ComponentModel.DataAnnotations;

namespace MovieApp.Server.Models
{
    public partial class Movie
    {
        public int MovieId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Overview { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = " This field accepts only positive numbers.")]
        public int Duration { get; set; }

        [Required]
        [Range(0, 10.0, ErrorMessage = "The value should be less than or equal to 10.")]
        public decimal? Rating { get; set; }

        public string PosterPath { get; set; }
    }
}
