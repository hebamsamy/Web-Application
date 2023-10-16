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
        public AccountManger(MyDBContext myDBContext, UserManager<User> _userManager) 
            : base(myDBContext) {
            userManager = _userManager;
        }

        public async Task<IdentityResult> SignUp(UserSignUpViewModel Viewmodel)
        {
            return await userManager.CreateAsync(Viewmodel.ToModel(),Viewmodel.Password);
        }
    }
}
