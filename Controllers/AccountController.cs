using ElSayedHotel.Models;
using ElSayedHotel.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ElSayedHotel.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewMode Newuser)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new();
                user.UserName = Newuser.Name;
                user.Address = Newuser.Address;
                user.PasswordHash = Newuser.Password;
                IdentityResult res =  await _userManager.CreateAsync(user);
                if(res.Succeeded)
                {
                    await _signInManager.SignInAsync(user , false);
                    return RedirectToAction("index", "home");
                }
                else
                {
                    foreach(var item in res.Errors)
                    {
                        ModelState.AddModelError("" , item.Description);
                    }
                    return View(Newuser);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
