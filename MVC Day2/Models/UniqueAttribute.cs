using MVC_Day2.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MVC_Day2.Models
{
    public class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            string? name = value?.ToString();

            // Get DbContext from DI container
            SchoolContext? context =
                validationContext.GetRequiredService<SchoolContext>();

            AddCourseViewModel? course =
                validationContext.ObjectInstance as AddCourseViewModel;

            if (context == null || course == null)
                return ValidationResult.Success;

            Course? SCourse =
                context.Courses.FirstOrDefault(
                    c => c.Name == name &&
                         c.Dept_Id == course.Dept_Id);

            if (SCourse == null)
                return ValidationResult.Success;

            return new ValidationResult(
                $"Name \"{name}\" Already Existed");
        }
    }
}