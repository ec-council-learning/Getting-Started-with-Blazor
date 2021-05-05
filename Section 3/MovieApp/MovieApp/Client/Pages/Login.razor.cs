using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
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
    public class LoginModel : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        CustomAuthStateProvider CustomAuthStateProvider { get; set; }

        [Inject]
        ILogger<Login> Logger { get; set; }

        protected UserLogin login = new();
        string returnUrl = "/";
        protected CustomValidator customValidator;

        protected override void OnInitialized()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var _returnUrl))
            {
                returnUrl = _returnUrl;
            }
        }

        protected async Task AuthenticateUser()
        {
            customValidator.ClearErrors();

            try
            {
                var response = await Http.PostAsJsonAsync("api/Login", login);
                var errors = await response.Content.ReadFromJsonAsync<Dictionary<string, List<string>>>();

                if (response.StatusCode == HttpStatusCode.BadRequest && errors.Count > 0)
                {
                    customValidator.DisplayErrors(errors);
                    throw new HttpRequestException($"Validation failed. Status Code: {response.StatusCode}");
                }
                else
                {
                    CustomAuthStateProvider.NotifyAuthStatus();
                    NavigationManager.NavigateTo(returnUrl);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
        }
    }
}
