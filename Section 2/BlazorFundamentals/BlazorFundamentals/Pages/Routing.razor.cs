using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFundamentals.Pages
{
    public class RoutingModel : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected string CurrentRoute { get; set; }

        protected override void OnParametersSet()
        {
            CurrentRoute = new Uri(NavigationManager.Uri).AbsolutePath;
        }

        protected void NavigateAway(int buttonNumber)
        {
            NavigationManager.NavigateTo("routingdemo/" + buttonNumber);
        }
    }
}
