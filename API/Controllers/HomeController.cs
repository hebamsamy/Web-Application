using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return new ObjectResult("Welcome");
        }
    }
}
