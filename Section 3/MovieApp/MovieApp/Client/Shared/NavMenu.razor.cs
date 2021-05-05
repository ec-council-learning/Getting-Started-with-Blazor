using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieApp.Client.Shared
{
    public class NavMenuModel : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        CustomAuthStateProvider CustomAuthStateProvider { get; set; }

        protected bool show = false;

        protected async Task LogoutUser()
        {
            await Http.GetAsync("api/login/logoutuser");
            CustomAuthStateProvider.NotifyAuthStatus();
            NavigationManager.NavigateTo("/");
        }
    }
}
