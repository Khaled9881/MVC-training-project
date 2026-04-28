using Microsoft.AspNetCore.Mvc;
using MVC_Day2.Models;
using MVC_Day2.Models.ViewModels;

namespace MVC_Day2.Controllers
{
    public class TraineeController : Controller
    {
        public CheckTraineeCourse checkTraineeCourse;
        public GetTraineeCourses getTraineeCourses;

        public TraineeController(CheckTraineeCourse _checkTraineeCourse, GetTraineeCourses getTraineeCourses)
        {
            checkTraineeCourse = _checkTraineeCourse;
            this.getTraineeCourses = getTraineeCourses;
        }

        public IActionResult GetProfile(int TraineeID, int CrsId)
        {
            TraineeCourseViewModel? tv = new TraineeCourseViewModel();
            tv = checkTraineeCourse.GetVM(TraineeID, CrsId);

            if (tv == null)
                return Content("Trainee Not Found");

            if (tv.Status == "Fail")
                ViewBag.color = "red";
            else
                ViewBag.color = "green";


            return View("TraineeCourse", tv);
        }

        public IActionResult GetCourses(int TraineeId)
        {
            GetTraineeCoursesViewModelcs? GV = getTraineeCourses.GET(TraineeId);
            if (GV == null)
                return Content("Not Found");

            return View("GetCourses", GV);
        }
    }
}
