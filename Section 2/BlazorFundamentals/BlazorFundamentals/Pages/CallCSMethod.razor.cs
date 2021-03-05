using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorFundamentals.Pages
{
    public class CallCSMethodModel : ComponentBase
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        #region Static .NET method call

        protected void InvokeJSMethod()
        {
            JSRuntime.InvokeVoidAsync("printMessageToConsole");
        }

        [JSInvokable]
        public static Task<string> PrintMessage()
        {
            return Task.FromResult("I am invoked from JavaScript.");
        }

        #endregion Static .NET method call


        #region Component instance method call

        private static Action<string> action;
        protected string message = "Click the button.";

        protected void InvokeDisplayMessageCallerJS(string value)
        {
            JSRuntime.InvokeVoidAsync("displayMessageCallerJS", value);
        }

        protected override void OnInitialized()
        {
            action = DisplayMessage;
        }

        private void DisplayMessage(string value)
        {
            message = value;
            StateHasChanged();
        }
        [JSInvokable]
        public static void DisplayMessageCaller(string value)
        {
            action.Invoke(value);
        }

        #endregion Component instance method call
    }

}
