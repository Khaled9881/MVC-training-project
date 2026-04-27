using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Day2.Models;
using MVC_Day2.Models.ViewModels;
using MVC_Day2.Repository;

namespace MVC_Day2.Controllers
{
    public class CourseController : Controller
    {

        ICourseRepository courseRepository;
        IDepartmentRepository departmentRepository;

        public CourseController(ICourseRepository _courseRepository, IDepartmentRepository _departmentRepository)
        {
            courseRepository = _courseRepository;
            departmentRepository = _departmentRepository;
        }

        private AddCourseViewModel LoadAddCourseViewModel()
        {
            return new AddCourseViewModel()
            {
                departments = departmentRepository.GetAll()
            };
        }

        public IActionResult Index()
        {
            //⚠️⚠️ is it right to have a view model as a return
            CourseIndexViewModel CVM = courseRepository.GetCoursesWithDeptNames();
            return View("Index", CVM);
        }

        public IActionResult ADD()
        {
            return View("AddCourse", LoadAddCourseViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ADDComplete(AddCourseViewModel course)
        {

            if (!ModelState.IsValid)
                return View("AddCourse", LoadAddCourseViewModel());

            Course newCourse = new Course()
            {
                Name = course.Name,
                Degree = course.Degree,
                MinDegree = course.MinDegree,
                Hours = course.Hours,
                Dept_Id = course.Dept_Id,
            };

            courseRepository.Add(newCourse);
            courseRepository.Save();
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
