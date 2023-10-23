using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Text;
using ViewModel;

namespace API.Controllers
{
    public class AccountController : ControllerBase
    {
        AccountManger accManger;
        public AccountController(AccountManger _accManger)
        {
            this.accManger = _accManger;
        }

        public async Task<IActionResult> SignIn([FromBody] UserSignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var Token = await accManger.SignIn(viewModel);
                if(Token == "IsLockedOut")
                {
                    return new JsonResult("Account Under Review!!");
                }
                else if (!string.IsNullOrEmpty(Token))
                {
                    return new JsonResult(new
                    {
                        massage = "You Successfully Logged in",
                        token = Token
                    }); 
                }
                else
                {
                    return new JsonResult("User name or Password Not Valid");
                }
            }
            var str = new StringBuilder();
            foreach (var item in ModelState.Values)
            {
                foreach (var item1 in item.Errors)
                {
                    str.Append(item1.ErrorMessage);
                }
            }

            return new JsonResult(str.ToString());
        }
    

        public async Task<IActionResult> SignUp([FromBody]UserSignUpViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await accManger.SignUp(viewModel);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    var str2 = new StringBuilder();
                    foreach (var item in result.Errors)
                    {
                        str2.Append(item.Description);
                    }
                    return new ObjectResult(str2);
                }
            }
            var str = new StringBuilder();
            foreach (var item in ModelState.Values)
            {
                foreach (var item1 in item.Errors)
                {
                    str.Append(item1.ErrorMessage);
                }
            }

            return new ObjectResult(str);
        }
    }

}
