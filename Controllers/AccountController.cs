using ElSayedHotel.Models;
using ElSayedHotel.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ElSayedHotel.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(RoleManager<IdentityRole>roleManager ,  UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            this.roleManager = roleManager;
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
                string role;
                if (signUpUser.accountType == 0)
                {
                    role = "User";
                }
                else if (signUpUser.accountType == 1)
                {
                    role = "Owner";
                }
                else if (signUpUser.accountType == 2 && User.IsInRole("Admin"))
                {
                    role = "Admin";
                }
                else
                {
                    ModelState.AddModelError("", "Try again");
                    return View(signUpUser);
                }
                ApplicationUser user = new()
                {
                    UserName = signUpUser.userName,
                    Email = signUpUser.email,
                    Address = signUpUser.Address,
                    NormalizedEmail = signUpUser.email.ToUpper(),
                    firstName = signUpUser.firstName,
                    lastName = signUpUser.lastName,
                    NormalizedUserName = signUpUser.userName.ToUpper(),
                    PhoneNumber = signUpUser.phoneNumber,
                    
                };
                IdentityResult createUserResult = await userManager.CreateAsync(user, signUpUser.password);
                if(createUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                    await userManager.AddClaimsAsync(user, [
                        new Claim(ClaimTypes.GivenName, signUpUser.firstName),
                       new Claim(ClaimTypes.Email , signUpUser.email),
                       new Claim(ClaimTypes.NameIdentifier , user.Id)
                    ]);
                    return RedirectToAction("login");
                }
                else
                {
                    foreach(var errors in createUserResult.Errors)
                        ModelState.AddModelError("", errors.Description);
                }
            }
            return View(signUpUser);
        }
        public IActionResult LogOut()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        [HttpGet]
        public IActionResult IsUserLoggedIn()
        {
            return Json(new { isLoggedIn = User.Identity.IsAuthenticated });
        }
    }
}
