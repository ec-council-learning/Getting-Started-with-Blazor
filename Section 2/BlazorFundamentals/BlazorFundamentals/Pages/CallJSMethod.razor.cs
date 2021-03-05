using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorFundamentals.Pages
{
    public class CallJSMethodModel : ComponentBase
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        protected int sumOfArray = 0;

        private int[] arrayItems = new int[] { 2, 4, 6, 8, 10 };

        protected ElementReference name;

        protected async Task GetSumOfArray()
        {
            sumOfArray = await JSRuntime.InvokeAsync<int>("getArraySum", arrayItems);
        }

        protected void ShowAlert()
        {
            JSRuntime.InvokeVoidAsync("alert", "I am invoked from .NET code");
        }

        protected void ShowArrayItems()
        {
            JSRuntime.InvokeVoidAsync("console.table", arrayItems);
        }

        protected async Task ShowName()
        {
            await JSRuntime.InvokeVoidAsync("showValue", name);
        }
    }
}
