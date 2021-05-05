using MovieApp.Server.Interfaces;
using MovieApp.Server.Models;
using System;
using System.Linq;

namespace MovieApp.Server.DataAccess
{
    public class WatchlistDataAccessLayer : IWatchlist
    {
        readonly MovieDBContext _dbContext;

        public WatchlistDataAccessLayer(MovieDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        string CreateWatchlist(int userId)
        {
            try
            {
                Watchlist watchlist = new()
                {
                    WatchlistId = Guid.NewGuid().ToString(),
                    UserId = userId,
                    DateCreated = DateTime.Now.Date
                };

                _dbContext.Watchlists.Add(watchlist);
                _dbContext.SaveChanges();

                return watchlist.WatchlistId;
            }
            catch
            {
                throw;
            }
        }

        public string GetWatchlistId(int userId)
        {
            try
            {
                Watchlist watchlist =
                    _dbContext.Watchlists.FirstOrDefault(x => x.UserId == userId);

                if (watchlist != null)
                {
                    return watchlist.WatchlistId;
                }
                else
                {
                    return CreateWatchlist(userId);
                }

            }
            catch
            {
                throw;
            }
        }

        public void ToggleWatchlistItem(int userId, int movieId)
        {
            string watchlistId = GetWatchlistId(userId);

            WatchlistItem existingWatchlistItem = _dbContext.WatchlistItems
                .FirstOrDefault(x => x.MovieId == movieId && x.WatchlistId == watchlistId);

            if (existingWatchlistItem != null)
            {
                _dbContext.WatchlistItems.Remove(existingWatchlistItem);
                _dbContext.SaveChanges();
            }
            else
            {
                WatchlistItem watchlistItem = new()
                {
                    WatchlistId = watchlistId,
                    MovieId = movieId,
                };

                _dbContext.WatchlistItems.Add(watchlistItem);
                _dbContext.SaveChanges();
            }
        }
    }
}
