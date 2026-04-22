using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Day2.Models;
using MVC_Day2.Models.ViewModels;

namespace MVC_Day2.Controllers
{
    public class CourseController : Controller
    {
        public SchoolContext context = new SchoolContext();
        public IActionResult Index()
        {
            var result = context.Courses.Select(i => new
            {
                Courses = i,
                DeptName = i.Department.Name
            }).ToList();

            CourseIndexViewModel CVM = new CourseIndexViewModel()
            {
                courses = result.Select(c => c.Courses).ToList(),
                CourseNames = result.Select(s => s.DeptName).ToList()
            };

            return View("Index", CVM);
        }

        public IActionResult ADD()
        {
            List<Department> departments = context.Departments.ToList();
            AddCourseViewModel ACV = new AddCourseViewModel()
            {
                departments = departments
            };
            return View("AddCourse", ACV);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ADDComplete(AddCourseViewModel course)
        {

            if (ModelState.IsValid == false)
            {
                course.departments = context.Departments.ToList();
                return View("AddCourse", course);
            }

            Course newCourse = new Course()
            {
                Name = course.Name,
                Degree = course.Degree,
                MinDegree = course.MinDegree,
                Hours = course.Hours,
                Dept_Id = course.Dept_Id,
            };

            //Course AddedCourse = context.Courses.FirstOrDefault(s => s.Id == course.Id);
            context.Courses.Add(newCourse);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult CheckMinDeg(int MinDegree, int Degree)
        {
            if (MinDegree < Degree)
                return Json(true);

            return Json("Minimum Degree must be less than total degree");
        }

    }

}
