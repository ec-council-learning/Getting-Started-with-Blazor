using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Server.Interfaces;
using MovieApp.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WatchlistController : ControllerBase
    {
        readonly IWatchlist _watchlistService;
        readonly IMovie _movieService;
        readonly IUser _userService;

        public WatchlistController(IWatchlist watchlistService, IMovie movieService, IUser userService)
        {
            _watchlistService = watchlistService;
            _movieService = movieService;
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<List<Movie>> Get(int userId)
        {
            return await Task.FromResult(GetUserWatchlist(userId)).ConfigureAwait(true);
        }

        List<Movie> GetUserWatchlist(int userId)
        {
            bool user = _userService.isUserExists(userId);
            if (user)
            {
                string watchlistid = _watchlistService.GetWatchlistId(userId);
                return _movieService.GetMoviesAvailableInWatchlist(watchlistid);
            }
            else
            {
                return new List<Movie>();
            }
        }

        [HttpGet]
        [Route("ToggleWatchlist/{userId}/{movieId}")]
        public async Task<List<Movie>> Get(int userId, int movieId)
        {
            _watchlistService.ToggleWatchlistItem(userId, movieId);
            return await Task.FromResult(GetUserWatchlist(userId)).ConfigureAwait(true);
        }
    }
}
