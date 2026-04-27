using Microsoft.EntityFrameworkCore;
using MVC_Day2.Models;
using MVC_Day2.Models.ViewModels;

namespace MVC_Day2.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolContext context;

        public CourseRepository(SchoolContext _context)
        {
            context = _context;
        }

        //Interface Implementation

        public IEnumerable<Course> GetAll()
        {
            return context.Courses.AsNoTracking().ToList();
        }

        public Course? GetById(int id)
        {
            return context.Courses.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Course course)
        {
            context.Courses.Add(course);
        }

        public void Delete(int id)
        {
            Course? course = GetById(id);
            if (course != null)
            {
                context.Courses.Remove(course);
            }
        }

        //public void Update(Course course)
        //{
        //    context.Courses.Update(course);
        //}


        public void Update(Course course)
        {
            if (course != null)
            {
                Course? EditedCourse = GetById(course.Id);
                if (EditedCourse != null)
                {
                    EditedCourse.Name = course.Name;
                    EditedCourse.Degree = course.Degree;
                    EditedCourse.MinDegree = course.MinDegree;
                    EditedCourse.Hours = course.Hours;
                    EditedCourse.Dept_Id = course.Dept_Id;
                }
            }

        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public CourseIndexViewModel GetCoursesWithDeptNames()
        {
            var result = context.Courses.Select(i => new
            {
                Courses = i,
                DeptName = i.Department.Name
            }).ToList();

            CourseIndexViewModel CVM = new CourseIndexViewModel()
            {
                courses = result.Select(c => c.Courses).ToList(),
                DepartmentNames = result.Select(s => s.DeptName).ToList()
            };

            return CVM;
        }
    }
}
