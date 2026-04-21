using Microsoft.AspNetCore.Mvc;
using MVC_Day2.Models;

namespace MVC_Day2.Controllers
{
    public class InstructorController : Controller
    {
        SchoolContext schoolContext = new SchoolContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllInstructors()
        {
            List<Instructor> instructors = schoolContext.Instructors.ToList();
            return View("GetAllInstructors", instructors);
        }

        public IActionResult Details(int id)
        {
            Instructor instructor = schoolContext.Instructors.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View("Details", instructor);
        }

    }
}
