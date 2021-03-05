using BlazorFundamentals.Models;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorFundamentals.Pages
{
    public class FormValidationModel : ComponentBase
    {

        protected User user = new();

        protected void HandleFormSubmit()
        {
            Console.WriteLine("Form submitted");
        }
    }
}
