using Microsoft.AspNetCore.Mvc;

namespace MVC_Day2.Controllers
{
    public class StateController : Controller
    {
        public StateController()
        {

        }
        public IActionResult SetSession(string name, int age)
        {

            HttpContext.Session.SetString("UserName", name);
            HttpContext.Session.SetInt32("Age", age);
            return Content("Session Saved Successfully");
        }
        public ActionResult GetSession()
        {
            string? n = HttpContext.Session.GetString("UserName");
            int? a = HttpContext.Session.GetInt32("Age");
            return Content($"MY name={n}\t MY Age={a}");
        }


        [HttpGet("SetCookies")]
        public IActionResult SetCookies(string name, int age)
        {
            HttpContext.Response.Cookies.Append("name", name);
            HttpContext.Response.Cookies.Append("age", age.ToString());

            return Content("Cookies Saved Successifully");
        }

        [HttpGet("GetCookies")]
        public ActionResult GetCookies()
        {
            string? name = HttpContext.Request.Cookies["name"];
            string? age = HttpContext.Request.Cookies["age"];

            return Content(name + "  " + age);
        }


    }
}
