using System;
using Microsoft.AspNetCore.Components;


namespace BlazorFundamentals.Pages
{
    public class DataBindingModel : ComponentBase
    {
        protected string SampleText = "This is a sample text depicting one-way data-binding";

        protected string Name { get; set; }

        protected DateTime SampleDate { get; set; } = new DateTime(2021, 3, 25);
    }
}
