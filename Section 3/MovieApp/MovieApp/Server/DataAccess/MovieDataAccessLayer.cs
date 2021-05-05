using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Interfaces;
using MovieApp.Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Server.DataAccess
{
    public class MovieDataAccessLayer : IMovie
    {
        readonly MovieDBContext _dbContext;

        public MovieDataAccessLayer(MovieDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddMovie(Movie movie)
        {
            try
            {
                _dbContext.Movies.Add(movie);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public List<Genre> GetGenre()
        {
            List<Genre> lstGenre = new();

            lstGenre = (from GenreList in _dbContext.Genres select GenreList).ToList();

            return lstGenre;
        }

        public List<Movie> GetAllMovies()
        {
            try
            {
                return _dbContext.Movies.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Movie GetMovieData(int movieId)
        {
            try
            {
                Movie movie = _dbContext.Movies.FirstOrDefault(x => x.MovieId == movieId);
                if (movie != null)
                {
                    _dbContext.Entry(movie).State = EntityState.Detached;
                    return movie;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public void UpdateMovie(Movie movie)
        {
            _dbContext.Entry(movie).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }


        public string DeleteMovie(int movieId)
        {
            try
            {
                Movie movie = _dbContext.Movies.Find(movieId);
                _dbContext.Movies.Remove(movie);
                _dbContext.SaveChanges();

                return (movie.PosterPath);
            }
            catch
            {
                throw;
            }
        }

        public List<Movie> GetMoviesAvailableInWatchlist(string watchlistID)
        {
            try
            {
                List<Movie> userWatchlist = new();
                List<WatchlistItem> watchlistItems =
                    _dbContext.WatchlistItems.Where(x => x.WatchlistId == watchlistID).ToList();

                foreach (WatchlistItem item in watchlistItems)
                {
                    Movie movie = GetMovieData(item.MovieId);
                    userWatchlist.Add(movie);
                }
                return userWatchlist;
            }
            catch
            {
                throw;
            }
        }
    }
}
