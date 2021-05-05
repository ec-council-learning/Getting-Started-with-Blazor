using Microsoft.AspNetCore.Components.Authorization;
using MovieApp.Client.Shared;
using MovieApp.Shared.Dto;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieApp.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly AppStateContainer _appStateContainer;

        public CustomAuthStateProvider(HttpClient httpClient, AppStateContainer appStateContainer)
        {
            _httpClient = httpClient;
            _appStateContainer = appStateContainer;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            UserLogin currentUser = await _httpClient.GetFromJsonAsync<UserLogin>("api/user/getcurrentuser");

            if (currentUser != null && currentUser.Username != null)
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, currentUser.Username),
                    new Claim("userId", currentUser.UserId.ToString()),
                    new Claim(ClaimTypes.Role, currentUser.UserTypeName),
                };

                var claimsIdentity = new ClaimsIdentity(userClaims, "BlazorClientAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await _appStateContainer.GetUserWatchlist(currentUser.UserId);

                return new AuthenticationState(claimsPrincipal);
            }
            else
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public void NotifyAuthStatus()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
