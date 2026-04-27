using Microsoft.EntityFrameworkCore;
using MVC_Day2.Models;
using MVC_Day2.Models.ViewModels;

namespace MVC_Day2.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly SchoolContext context;

        public InstructorRepository(SchoolContext _context)
        {
            context = _context;
        }


        //Interface Implementation
        public IEnumerable<Instructor> GetAll()
        {
            return context.Instructors;
        }

        public Instructor? GetById(int id)
        {
            return context.Instructors.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Instructor instructor)
        {
            context.Instructors.Add(instructor);
        }

        public void Delete(int id)
        {
            Instructor? instructor = GetById(id);
            if (instructor != null)
            {
                context.Instructors.Remove(instructor);
            }
        }

        public void Update(Instructor instructor)
        {
            context.Instructors.Update(instructor);
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public AllInstructorsViewModel Get_ALL_Instructor_Relevant_Data(int pageNumber)
        {
            int InstructorsCount = context.Instructors.Count();
            int PagesCount = (int)Math.Ceiling(InstructorsCount / 10.0);

            var result = context.Instructors.Skip((pageNumber - 1) * 10).Take(10)
               .Select(i => new
               {
                   Instructor = i,
                   DeptName = i.Department.Name != null ? i.Department.Name : "N/A",
                   Courses = i.Course.Name != null ? i.Course.Name : "N/A"
               }).ToList();

            AllInstructorsViewModel allInstructorsViewModel = new AllInstructorsViewModel()
            {
                instructors = result.Select(r => r.Instructor).ToList(),
                DepartmentNames = result.Select(r => r.DeptName).ToList(),
                CourseNames = result.Select(r => r.Courses).ToList(),
                CurrentPage = pageNumber,
                PagesCount = PagesCount
            };

            return allInstructorsViewModel;
        }

        public AllInstructorsViewModel GetInstrucotrCourseDeptNames(int id)
        {
            var result = context.Instructors.Where(i => i.Id == id)
                .Select(i => new
                {
                    Instructor = i,
                    DepartmentName = i.Department.Name,
                    CourseName = i.Course.Name
                });

            AllInstructorsViewModel allInstructorsViewModel = new AllInstructorsViewModel()
            {
                instructors = result.Select(r => r.Instructor).ToList(),
                DepartmentNames = result.Select(r => r.DepartmentName).ToList(),
                CourseNames = result.Select(r => r.CourseName).ToList()
            };

            return allInstructorsViewModel;


        }
    }
}
