using System.ComponentModel.DataAnnotations;

namespace MVC_Day2.Models
{
    public class divideAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            int? hours = (int)value;
            if (hours != 0 && (hours % 3 == 0))
                return ValidationResult.Success;

            return new ValidationResult("Hourse must be divisible by 3");
        }
    }
}
