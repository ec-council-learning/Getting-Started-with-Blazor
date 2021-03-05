using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFundamentals.Pages
{
    public class EventHandlingModel : ComponentBase
    {
        protected string Gender { get; set; }

        protected void ButtonClicked()
        {
            Console.WriteLine("button clicked");
        }

        protected void SelectGender(ChangeEventArgs e)
        {
            Gender = e.Value.ToString();
        }

        protected void FocusTextbox(int textboxNumber)
        {
            Console.WriteLine($"You have selected textbox number {textboxNumber}");
        }
    }
}
