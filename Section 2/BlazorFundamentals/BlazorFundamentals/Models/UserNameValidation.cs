using System.ComponentModel.DataAnnotations;

namespace BlazorFundamentals.Models
{
    public class UserNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            if (!value.ToString().ToLower().Contains("admin"))
            {
                return null;
            }

            return new ValidationResult("UserName cannot contain the word admin",
                new[] { validationContext.MemberName });
        }
    }
}
