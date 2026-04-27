using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Day2.Models;
using MVC_Day2.Models.ViewModels;
using MVC_Day2.Repository;

namespace MVC_Day2.Controllers
{
    public class InstructorController : Controller
    {
        IInstructorRepository instructorRepository;
        ICourseRepository courseRepository;
        IDepartmentRepository departmentRepository;


        public InstructorController(IInstructorRepository instructorRepository,
        ICourseRepository courseRepository,
        IDepartmentRepository departmentRepository)
        {
            this.instructorRepository = instructorRepository;
            this.courseRepository = courseRepository;
            this.departmentRepository = departmentRepository;
        }

        public IActionResult GetAllInstructors(int pageNumber = 1)
        {
            return View("GetAllInstructors",
                instructorRepository.Get_ALL_Instructor_Relevant_Data(pageNumber));
        }

        public IActionResult Details(int id)
        {

            AllInstructorsViewModel? allInstructorsViewModel =
                instructorRepository.GetInstrucotrCourseDeptNames(id);

            if (allInstructorsViewModel == null)
                return NotFound();

            return View("Details", allInstructorsViewModel);
        }


        public IActionResult AddInstructor()
        {
            DepartmentsCoursesViewModel departmentsCoursesViewModel = new DepartmentsCoursesViewModel();
            departmentsCoursesViewModel.Departments = departmentRepository.GetAll();
            departmentsCoursesViewModel.Courses = courseRepository.GetAll();
            return View("AddInstructor", departmentsCoursesViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveInstructor(Instructor instructor, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                var vm = new DepartmentsCoursesViewModel
                {
                    Departments = departmentRepository.GetAll(),
                    Courses = courseRepository.GetAll()
                };
                return View("AddInstructor", vm);
            }



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

            instructorRepository.Add(newInstructor);
            instructorRepository.Save();
            return RedirectToAction("GetAllInstructors");
        }

        public ActionResult EditInstructor(int id)
        {

            var instructor = instructorRepository.GetAll().Where(i => i.Id == id).ToList();

            AllInstructorsViewModel allInstructorsViewModel = new AllInstructorsViewModel()
            {
                instructors = instructor,
                DepartmentNames = departmentRepository.GetAll().Select(d => d.Name).ToList(),
                CourseNames = courseRepository.GetAll().Select(c => c.Name).ToList()
            };

            if (allInstructorsViewModel == null)
            {
                return NotFound();
            }

            ViewBag.Departments = departmentRepository.GetAll();
            ViewBag.Courses = courseRepository.GetAll();
            return View("EditInstructor", allInstructorsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(Instructor instructor, IFormFile ImageFile)
        {

            // Find the existing instructor by ID
            var existingInstructor = instructorRepository.GetById(instructor.Id);

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

            instructorRepository.Save();
            return RedirectToAction("GetAllInstructors");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteInstructor(int id)
        {
            instructorRepository.Delete(id);
            instructorRepository.Save();
            return RedirectToAction("GetAllInstructors");
        }


    }
}
