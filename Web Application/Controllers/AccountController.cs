using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using ViewModel;

namespace Web_Application.Controllers
{
    public class AccountController : Controller
    {
        AccountManger accManger;
        public AccountController(AccountManger _accManger)
        {
            accManger = _accManger;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                IdentityResult result= await  accManger.SignUp(viewModel);
                if (result.Succeeded)
                {
                     return RedirectToAction("Index","Product");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View();
                }
            }
            return View();
        }
    }
}
