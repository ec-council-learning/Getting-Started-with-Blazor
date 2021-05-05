using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MovieApp.Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Client.Pages
{
    public class AddEditMovieModel : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int MovieID { get; set; }

        protected string Title = "Add";
        public Movie movie = new();
        protected List<Genre> lstGenre = new();
        protected string imagePreview;
        const int MaxFileSize = 10 * 1024 * 1024; // 10 MB
        const string DefaultStatus = "Maximum size allowed for the image is 10 MB";
        protected string status = DefaultStatus;

        protected override async Task OnInitializedAsync()
        {
            await GetGenreList();
        }

        protected async Task GetGenreList()
        {
            lstGenre = await Http.GetFromJsonAsync<List<Genre>>("api/Movie/GetGenreList");
        }

        protected override async Task OnParametersSetAsync()
        {
            if (MovieID != 0)
            {
                Title = "Edit";
                movie = await Http.GetFromJsonAsync<Movie>("api/Movie/" + MovieID);
                imagePreview = "/Poster/" + movie.PosterPath;
            }
        }

        protected async Task SaveMovie()
        {
            if (movie.MovieId != 0)
            {
                await Http.PutAsJsonAsync("api/Movie", movie);
            }
            else
            {
                await Http.PostAsJsonAsync("api/Movie", movie);
            }
            NavigationManager.NavigateTo("/admin/movies");
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/admin/movies");
        }

        protected async Task ViewImage(InputFileChangeEventArgs e)
        {
            if (e.File.Size > MaxFileSize)
            {
                status = $"The file size is {e.File.Size} bytes, this is more than the allowed limit of {MaxFileSize} bytes.";
                return;
            }
            else if (!e.File.ContentType.Contains("image"))
            {
                status = "Please upload a valid image file";
                return;
            }
            else
            {
                using var reader = new StreamReader(e.File.OpenReadStream(MaxFileSize));

                var format = "image/jpeg";
                var imageFile = await e.File.RequestImageFileAsync(format, 640, 480);

                using var fileStream = imageFile.OpenReadStream(MaxFileSize);
                using var memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream);

                imagePreview = $"data:{format};base64,{Convert.ToBase64String(memoryStream.ToArray())}";
                movie.PosterPath = Convert.ToBase64String(memoryStream.ToArray());

                status = DefaultStatus;
            }
        }
    }
}
