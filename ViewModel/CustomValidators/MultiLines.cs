using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class MultiLines :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string temp = value?.ToString();
            if (string.IsNullOrEmpty(temp))
            {
                return new ValidationResult("Please, Provide a valid value");
            }
            else if (temp.Split(Environment.NewLine).Length < 2)
            {
                return new ValidationResult("Please, Provide a Multi line");
            }
            return ValidationResult.Success;
        }
    }
}
