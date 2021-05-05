using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieApp.Server.Interfaces;
using MovieApp.Server.Models;
using MovieApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MovieApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        readonly IWebHostEnvironment _hostingEnvironment;
        readonly IMovie _movieService;
        readonly IConfiguration _config;
        readonly string posterFolderPath = string.Empty;

        public MovieController(IConfiguration config, IWebHostEnvironment hostingEnvironment, IMovie movieService)
        {
            _config = config;
            _movieService = movieService;
            _hostingEnvironment = hostingEnvironment;
            posterFolderPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Poster");
        }

        [HttpGet]
        [Route("GetGenreList")]
        public async Task<IEnumerable<Genre>> GenreList()
        {
            return await Task.FromResult(_movieService.GetGenre());
        }

        [HttpPost]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult Post(Movie movie)
        {
            if (!string.IsNullOrEmpty(movie.PosterPath))
            {
                string fileName = Guid.NewGuid() + ".jpg";
                string fullPath = Path.Combine(posterFolderPath, fileName);

                byte[] imageBytes = Convert.FromBase64String(movie.PosterPath);
                System.IO.File.WriteAllBytes(fullPath, imageBytes);

                movie.PosterPath = fileName;
            }
            else
            {
                movie.PosterPath = _config["DefaultPoster"];
            }

            _movieService.AddMovie(movie);

            return Ok();
        }

        [HttpGet]
        public async Task<List<Movie>> Get()
        {
            return await Task.FromResult(_movieService.GetAllMovies());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Movie movie = _movieService.GetMovieData(id);
            if (movie != null)
            {
                return Ok(movie);
            }
            return NotFound();
        }

        [HttpPut]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult Put(Movie movie)
        {
            bool IsBase64String = CheckBase64String(movie.PosterPath);

            if (IsBase64String)
            {
                string fileName = Guid.NewGuid() + ".jpg";
                string fullPath = Path.Combine(posterFolderPath, fileName);

                byte[] imageBytes = Convert.FromBase64String(movie.PosterPath);
                System.IO.File.WriteAllBytes(fullPath, imageBytes);

                movie.PosterPath = fileName;
            }

            _movieService.UpdateMovie(movie);

            return Ok();
        }

        static bool CheckBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult Delete(int id)
        {
            string coverFileName = _movieService.DeleteMovie(id);
            if (coverFileName != _config["DefaultPoster"])
            {
                string fullPath = Path.Combine(posterFolderPath, coverFileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            return Ok();
        }
    }
}
