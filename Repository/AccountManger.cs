using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Repository
{
    public class AccountManger:MainManager<User>
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        public AccountManger(MyDBContext myDBContext, UserManager<User> _userManager, SignInManager<User> _signInManager) 
            : base(myDBContext) {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public async Task<IdentityResult> SignUp(UserSignUpViewModel Viewmodel)
        {
            return await userManager.CreateAsync(Viewmodel.ToModel(),Viewmodel.Password);
        }
        public async Task<SignInResult> SignIn (UserSignInViewModel viewModel)
        {
           return await signInManager.PasswordSignInAsync(viewModel.UserName, 
                  viewModel.Password,viewModel.RememberMe,false);
        }
        public async void SignOut() {
            await signInManager.SignOutAsync();
        }
    }
}
