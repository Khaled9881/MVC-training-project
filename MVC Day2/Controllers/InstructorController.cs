using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Day2.Models;
using MVC_Day2.Models.ViewModels;

namespace MVC_Day2.Controllers
{
    public class InstructorController : Controller
    {
        SchoolContext schoolContext = new SchoolContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllInstructors(int pageNumber = 1)
        {
            int InstructorsCount = schoolContext.Instructors.Count();
            int PagesCount = (int)Math.Ceiling(InstructorsCount / 10.0);

            //if(pageNumber >= PagesCount)
            //{

            //}

            var result = schoolContext.Instructors.Skip((pageNumber - 1) * 10).Take(10)
                .Select(i => new
                {
                    Instructor = i,
                    DeptName = i.Department.Name,
                    Courses = i.Course.Name
                }).ToList();

            AllInstructorsViewModel allInstructorsViewModel = new AllInstructorsViewModel()
            {
                instructors = result.Select(r => r.Instructor).ToList(),
                DepartmentNames = result.Select(r => r.DeptName).ToList(),
                CourseNames = result.Select(r => r.Courses).ToList(),
                CurrentPage = pageNumber,
                PagesCount = PagesCount
            };

            return View("GetAllInstructors", allInstructorsViewModel);
        }

        public IActionResult Details(int id)
        {

            var result = schoolContext.Instructors.Where(i => i.Id == id)
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

            if (allInstructorsViewModel == null)
            {
                return NotFound();
            }
            return View("Details", allInstructorsViewModel);
        }

        public IActionResult AddInstructor(int id)
        {
            DepartmentsCoursesViewModel departmentsCoursesViewModel = new DepartmentsCoursesViewModel();
            departmentsCoursesViewModel.Departments = schoolContext.Departments.ToList();
            departmentsCoursesViewModel.Courses = schoolContext.Courses.ToList();
            return View("AddInstructor", departmentsCoursesViewModel);
        }

        public IActionResult SaveInstructor(Instructor instructor, IFormFile ImageFile)
        {

            var fileName = ImageFile.FileName;

            Instructor newInstructor = new Instructor()
            {
                Name = instructor.Name,
                Salary = instructor.Salary,
                Address = instructor.Address,
                Dept_ID = instructor.Dept_ID,
                Crs_ID = instructor.Crs_ID,
                Image = "\\Images\\" + fileName,
            };

            schoolContext.Instructors.Add(newInstructor);
            schoolContext.SaveChanges();
            return RedirectToAction("GetAllInstructors");
        }

        public ActionResult EditInstructor(int id)
        {

            var instructor = schoolContext.Instructors.Where(i => i.Id == id).ToList();

            AllInstructorsViewModel allInstructorsViewModel = new AllInstructorsViewModel()
            {
                instructors = instructor,
                DepartmentNames = schoolContext.Departments.Select(d => d.Name).ToList(),
                CourseNames = schoolContext.Courses.Select(c => c.Name).ToList()
            };

            if (allInstructorsViewModel == null)
            {
                return NotFound();
            }

            ViewBag.Departments = schoolContext.Departments.ToList();
            ViewBag.Courses = schoolContext.Courses.ToList();
            return View("EditInstructor", allInstructorsViewModel);


            //Instructor instructor = schoolContext.Instructors.Find(id);
            //return View("EditInstructor", instructor);
        }


        public IActionResult SaveEdit(Instructor instructor, IFormFile ImageFile)
        {

            // Find the existing instructor by ID
            var existingInstructor = schoolContext.Instructors.Find(instructor.Id);

            if (existingInstructor == null)
            {
                return NotFound();
            }

            // Update the properties
            existingInstructor.Name = instructor.Name;
            existingInstructor.Salary = instructor.Salary;
            existingInstructor.Address = instructor.Address;
            existingInstructor.Dept_ID = instructor.Dept_ID;
            existingInstructor.Crs_ID = instructor.Crs_ID;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = ImageFile.FileName;
                existingInstructor.Image = "\\Images\\" + fileName;
            }

            schoolContext.SaveChanges();
            return RedirectToAction("GetAllInstructors");
        }


        public IActionResult DeleteInstructor(int id)
        {
            Instructor instructor = schoolContext.Instructors.Find(id);

            schoolContext.Instructors.Remove(instructor);
            schoolContext.SaveChanges(true);
            return RedirectToAction("GetAllInstructors");

        }


    }
}
