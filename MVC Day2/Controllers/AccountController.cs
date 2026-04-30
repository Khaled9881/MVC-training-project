using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_Day2.Models;
using MVC_Day2.Models.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC_Day2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("GetAllInstructors", "Instructor");
        }


        //Register USER

        //[HttpGet]
        public IActionResult Register()
        {
            return View("Register", new AccountRegisterViewModel
            {
                Username = string.Empty,
                Email = string.Empty,
                Password = string.Empty,
                ConfirmPassword = string.Empty,
                Address = string.Empty
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registered(AccountRegisterViewModel accountRegisterViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = accountRegisterViewModel.Username,
                    Email = accountRegisterViewModel.Email,
                    //PasswordHash = accountRegisterViewModel.Password,
                    Address = accountRegisterViewModel.Address,
                };

                IdentityResult result = await userManager.CreateAsync(user, accountRegisterViewModel.Password);

                if (result.Succeeded)
                {
                    IdentityResult roleResult = await userManager.AddToRoleAsync(user,
                        "Seller");

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("GetAllInstructors", "Instructor");
                }

                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
            }

            return View("Register", accountRegisterViewModel);

        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View("login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logged(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(loginViewModel.UserName);

                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, loginViewModel.Password);

                    if (found)
                    {
                        List<Claim> extraClaims = new List<Claim>();
                        extraClaims.Add(new Claim("Address", user.Address));

                        await signInManager.SignInWithClaimsAsync(user, loginViewModel.RememberMe, extraClaims);

                        return RedirectToAction("GetAllInstructors", "Instructor");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid Account Credentials");
            return View("LogIn", loginViewModel);
        }


        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LogIn");

        }


    }
}
