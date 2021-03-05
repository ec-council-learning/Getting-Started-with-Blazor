using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFundamentals.Pages
{
    public class RouteparameterModel : ComponentBase
    {
        [Parameter]
        public int RouteParam { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected void NavigateBack()
        {
            NavigationManager.NavigateTo("route1");
        }
    }
}
