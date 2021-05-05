using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MovieApp.Client.Shared;
using MovieApp.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace MovieApp.Client.Pages
{
    public class RegistrationModel : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        ILogger<Registration> Logger { get; set; }

        protected UserRegistration registration = new();

        protected CustomValidator registerValidator;

        protected async Task RegisterUser()
        {
            registerValidator.ClearErrors();

            try
            {
                var response = await Http.PostAsJsonAsync("api/User", registration);
                var errors = await response.Content.ReadFromJsonAsync<Dictionary<string, List<string>>>();

                if (response.StatusCode == HttpStatusCode.BadRequest && errors.Count > 0)
                {
                    registerValidator.DisplayErrors(errors);
                    throw new HttpRequestException($"Validation failed. Status Code: {response.StatusCode}");
                }
                else
                {
                    NavigationManager.NavigateTo("/login");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
        }
    }
}
