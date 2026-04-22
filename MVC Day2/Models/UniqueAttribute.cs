using MVC_Day2.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MVC_Day2.Models
{
    public class UniqueAttribute : ValidationAttribute
    {
        SchoolContext context = new SchoolContext();
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? name = value?.ToString();
            AddCourseViewModel? course = validationContext.ObjectInstance as AddCourseViewModel;
            Course? SCourse = context.Courses.FirstOrDefault(c => c.Name == name && c.Dept_Id == course.Dept_Id);

            if (SCourse == null)
                return ValidationResult.Success;

            return new ValidationResult($"Name \"{name}\" Already Existed");

        }
    }
}
