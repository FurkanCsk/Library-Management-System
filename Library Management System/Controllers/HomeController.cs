using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class HomeController : Controller
    {
        // SignUp  and LogIn page
        public IActionResult Index()
        {
            return View();
        }

        //  About page
        public IActionResult About()
        {
            return View();
        }

        // Choice page after LogIn
        public IActionResult HomePage()
        {
            return View();
        }
    }
}
