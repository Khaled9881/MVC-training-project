namespace MVC_Day2.Models.ViewModels
{
    public class DepartmentsCoursesViewModel
    {
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Course> Courses { get; set; }

    }
}
