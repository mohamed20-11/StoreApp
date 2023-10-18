using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using ViewModel;

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
