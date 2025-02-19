using ElSayedHotel.Models;
using ElSayedHotel.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ElSayedHotel.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel logInUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser? user;
                EmailAddressAttribute email = new();
                if (email.IsValid(logInUser.UserName))
                {
                    user = await userManager.FindByEmailAsync(logInUser.UserName);
                }
                else
                {
                    user = await userManager.FindByNameAsync(logInUser.UserName);
                }

                if(user is not null)
                {
                    if(await userManager.CheckPasswordAsync(user , logInUser.Password))
                    {
                        await signInManager.SignInAsync(user, isPersistent: logInUser.RememberMe);
                        return RedirectToAction("index", "home");
                    }
                }
                ModelState.AddModelError("", "Username or password is invalid");
            }
            return View(logInUser);
        }

        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> signup(SignUpViewModel signUpUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    UserName = signUpUser.userName,
                    Email = signUpUser.email,
                    Address = signUpUser.Address,
                    NormalizedEmail = signUpUser.email.ToUpper()
                };
                IdentityResult createUserResult = await userManager.CreateAsync(user, signUpUser.password);
                if(createUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("login");
                }
                else
                {
                    ModelState.AddModelError("", "Cannot handle your request right now , please try again later!");
                }
            }
            return View(signUpUser);
        }
    }
}
