namespace MVC_Day2.Models.ViewModels
{
    public class AllInstructorsViewModel
    {
        public List<Instructor> instructors { get; set; }
        public List<string> DepartmentNames { get; set; }
        public List<string> CourseNames { get; set; }
        public int? CurrentPage { get; set; }
        public int? PagesCount { get; set; } = 1;
    }
}
