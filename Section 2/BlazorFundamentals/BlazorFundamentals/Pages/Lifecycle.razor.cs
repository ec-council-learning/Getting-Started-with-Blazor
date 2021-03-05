using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFundamentals.Pages
{
    public class LifecycleModel : ComponentBase
    {
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            Console.WriteLine("SetParametersAsync-start");
            await base.SetParametersAsync(parameters);
            Console.WriteLine("SetParametersAsync-end");
        }

        protected override void OnInitialized()
        {
            Console.WriteLine("OnInitialized-start");
            base.OnInitialized();
            Console.WriteLine("OnInitialized-end");
        }

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("OnInitializedAsync-start");
            await base.OnInitializedAsync();
            Console.WriteLine("OnInitializedAsync-end");
        }

        protected override void OnParametersSet()
        {
            Console.WriteLine("OnParametersSet-start");
            base.OnParametersSet();
            Console.WriteLine("OnParametersSet-end");
        }

        protected override async Task OnParametersSetAsync()
        {
            Console.WriteLine("OnParametersSetAsync-start");
            await base.OnParametersSetAsync();
            Console.WriteLine("OnParametersSetAsync-end");
        }

        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine("OnAfterRender({0})-start", firstRender);
            base.OnAfterRender(firstRender);
            Console.WriteLine("OnAfterRender({0})-end", firstRender);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine("OnAfterRenderAsync({0})-start", firstRender);
            await base.OnAfterRenderAsync(firstRender);
            Console.WriteLine("OnAfterRenderAsync({0})-end", firstRender);
        }
    }
}
