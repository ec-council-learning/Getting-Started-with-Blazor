using BlazorFundamentals.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace BlazorFundamentals.Pages
{
    public class EditContextValidationModel : ComponentBase
    {
        protected User editContextuser = new();
        protected EditContext editFormContext;
        protected bool isFormValid { get; set; }

        protected override void OnInitialized()
        {
            editFormContext = new EditContext(editContextuser);
            editFormContext.OnFieldChanged += HandleFormFieldChangedEvent;
        }

        protected void HandleFormFieldChangedEvent(object sender, FieldChangedEventArgs e)
        {
            var isFormComplete = editFormContext.IsModified(() => editContextuser.Name)
            && editFormContext.IsModified(() => editContextuser.Age)
            && editFormContext.IsModified(() => editContextuser.UserName);

            isFormValid = isFormComplete && editFormContext.Validate();
        }

        protected void HandleEditContextFormSubmit()
        {
            var isFormValid = editFormContext.Validate();

            if (isFormValid)
            {
                Console.WriteLine("The form is Valid");
            }
            else
            {
                Console.WriteLine("The form is Invalid");
            }
        }

        public void Dispose()
        {
            editFormContext.OnFieldChanged -= HandleFormFieldChangedEvent;
        }

    }
}
