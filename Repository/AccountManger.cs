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
        public AccountManger(MyDBContext myDBContext,
            UserManager<User> _userManager, 
            SignInManager<User> _signInManager
         ) 
            : base(myDBContext) {
            userManager = _userManager;
            signInManager = _signInManager;
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
        public async Task<SignInResult> SignIn (UserSignInViewModel viewModel)
        {
           return await signInManager.PasswordSignInAsync(viewModel.UserName, 
                  viewModel.Password,viewModel.RememberMe,true);
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
