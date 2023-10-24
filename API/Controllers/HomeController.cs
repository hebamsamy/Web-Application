using Microsoft.AspNetCore.Mvc;
using ViewModel;

namespace API.Controllers
{
    //Home/index
    public class HomeController : ControllerBase
    {
        [Route("Welcome")]
        [Route("Home")]
        public IActionResult Index()
        {
            return new ObjectResult("Welcome");
        }
        //get/3
        //get/101
        [Route("Get/{id:int}/{Name:alpha}")]
        [HttpPost]
        public IActionResult Get(int id, string Name) {
            return new ObjectResult($"Your ID Is {id} an Name Is {Name}");

        }

        [HttpPost("add")]
        public IActionResult Get([FromBody] CartItemViewModel viewModel) {
            return new ObjectResult("Add");
        }

    }
}
