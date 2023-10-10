using Microsoft.AspNetCore.Mvc;

namespace Web_Application.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
            //return View("PendingPage");
        }
        public IActionResult PendingPage()
        {
            return View();
        }

        //public ViewResult ContactU
    }
}
