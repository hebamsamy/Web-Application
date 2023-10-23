using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using LinqKit;

namespace Repository
{
    public class AccountManger:MainManager<User>
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        IConfiguration configuration;
        public AccountManger(MyDBContext myDBContext,
            UserManager<User> _userManager, 
            SignInManager<User> _signInManager,
            IConfiguration _configuration
         ) 
            : base(myDBContext) {
            userManager = _userManager;
            signInManager = _signInManager;
            configuration = _configuration;
        }

        public async Task<IdentityResult> SignUp(UserSignUpViewModel Viewmodel)
        {
            var model = Viewmodel.ToModel();
            var result = await userManager.CreateAsync(model,Viewmodel.Password);
            if (result.Succeeded)
            {
                result = await userManager.AddToRoleAsync(model, Viewmodel.Role);
            }
            return result;

        }
        public async Task<string> SignIn (UserSignInViewModel viewModel)
        {
            List<Claim> claims = new List<Claim>();
            var user= await userManager.FindByNameAsync(viewModel.UserName);

            if (user != null)
            {
                var roles= await userManager.GetRolesAsync(user);
                roles.ForEach(role =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));

                });
                claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id));

            
               var result=  await signInManager.PasswordSignInAsync(viewModel.UserName, 
                      viewModel.Password,viewModel.RememberMe,true);
                if(result.Succeeded)
                {
                    //make token
                    //Jwt => Json Web Token  // Bearer
               
                    JwtSecurityToken jwtSecurity = new JwtSecurityToken(
                       claims: claims,
                       expires:DateTime.Now.AddDays(1),
                       signingCredentials: new SigningCredentials(
                                key: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Key"]!)),
                                algorithm: SecurityAlgorithms.HmacSha256
                            ));
                   return new JwtSecurityTokenHandler().WriteToken(jwtSecurity);
                }
                else if (result.IsLockedOut)
                {
                    return "IsLockedOut";
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
        public async void SignOut() {
            await signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePassword(UserChangePasswordViewModel viewModel) {
            var user = await userManager.FindByIdAsync(viewModel.Id); 
            if (user != null) {
               return await userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Description="User Not Found"
            });
        }

        public async Task<string> GetForgotPasswordCode(string Email) {
            var user = await userManager.FindByEmailAsync(Email);
            if(user != null)
            {
                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                return code;
            }
            return string.Empty;
        }
        public async Task<IdentityResult> ForgotPassword(UserForgotPasswordViewModel viewModel)
        {
            var user = await userManager.FindByEmailAsync(viewModel.Email);
            if (user != null)
            {
                return await userManager.ResetPasswordAsync(user,viewModel.Code,viewModel.NewPassword);
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Description = "User Not Found"
            });
        }
        public async Task<IdentityResult> AssignRolesToUser(string UserId,List<string> roles)
        {
            var user= await userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                return await userManager.AddToRolesAsync(user, roles);
            }
            return new IdentityResult();
        } 

    }
}
