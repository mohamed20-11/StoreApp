using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using ViewModel;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web_Application.Controllers
{
    public class AccountController : Controller
    {
        AccountManger accManger;
        RoleManager roleManager;
        public AccountController(AccountManger _accManger,RoleManager _roleManager)
        {
            accManger = _accManger;
            roleManager = _roleManager;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            ViewData["list"] = RoleList();
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
                     return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    ViewData["list"] = RoleList();
                    return View();
                }
            }
            ViewData["list"] = RoleList();
            return View();
        }

        [HttpGet]
        public IActionResult SignIn(string ReturnUrl = "/")
        {
            //var veiwModel = new UserSignInViewModel() { ReturnUrl= ReturnUrl };
            //return View(veiwModel);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var result = await accManger.SignIn(viewModel);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your Account is Under Reivew");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Invaild User Name Or Password");
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult SignOut()
        {
             accManger.SignOut();
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            
            ViewBag.Success = false;
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserChangePasswordViewModel viewModel)
        {
            viewModel.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                var result = await accManger.ChangePassword(viewModel);
                if (result.Succeeded)
                {
                     ViewBag.Success = true;
                }
                return View();
            }
            ViewBag.Success = false;
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            ViewBag.Success = false;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if(!string.IsNullOrEmpty(Email))
            {
               string code= await accManger.GetForgotPasswordCode(Email);
                if (string.IsNullOrEmpty(code))
                {
                    ViewBag.Success = false;
                }
                else
                {
                    //Send Mail With Code
                    var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                    {
                        Credentials = new NetworkCredential("dfeb7400a34af3", "bebc3fd59ccb80"),
                        EnableSsl = true
                    };
                    client.Send("from@example.com", Email, "Forget Password Verification", $"Your Code is {code}");
                    ViewBag.Success = true;
                }
                return View();
            }
            ViewBag.Success = false;
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPasswordVerification()
        {
            ViewBag.Success = false;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordVerification(UserForgotPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accManger.ForgotPassword(viewModel);
                if (!result.Succeeded)
                {
                    ViewBag.Success = false;
                }
                else
                {
                    ViewBag.Success = true;
                }
                return View();
            }
            ViewBag.Success = false;
            return View();
        }
        private List<SelectListItem> RoleList()
        {
            return roleManager.GetList().Select(r => new SelectListItem()
            {
                Value = r.Name,
                Text  = r.Name
            }).ToList();
        }
    }
}
