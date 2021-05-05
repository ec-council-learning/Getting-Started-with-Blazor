using MovieApp.Server.Models;
using System.Collections.Generic;

namespace MovieApp.Server.Interfaces
{
    public interface IMovie
    {
        void AddMovie(Movie movie);

        List<Genre> GetGenre();

        List<Movie> GetAllMovies();

        Movie GetMovieData(int movieId);

        void UpdateMovie(Movie movie);

        string DeleteMovie(int movieId);

        List<Movie> GetMoviesAvailableInWatchlist(string watchlistID);
    }
}
