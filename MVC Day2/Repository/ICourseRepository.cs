using MVC_Day2.Models;
using MVC_Day2.Models.ViewModels;

namespace MVC_Day2.Repository
{
    public interface ICourseRepository : IRepository<Course>
    {
        public CourseIndexViewModel GetCoursesWithDeptNames();
    }
}
