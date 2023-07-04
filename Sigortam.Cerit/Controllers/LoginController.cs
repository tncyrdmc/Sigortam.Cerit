using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sigortam.Cerit.Common.Dtos.User;
using Sigortam.Cerit.Data.Entity;

namespace Sigortam.Cerit.Controllers
{
   
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            //var pass = new PasswordHasher<object>().HashPassword(null, "2183579Mus*");

            //var passCheck = new PasswordHasher<object>().VerifyHashedPassword(null, user.PasswordHash, "2183579Mus*");


            //if (user != null && user.AgreementEndDate > DateTime.Now)
            //{
                var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Insurance");
                }
            //}

            return View();
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Register(UserLoginDto userPostDto)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = userPostDto.UserName, Email = userPostDto.Email , Name = userPostDto.FirstName , Surname = userPostDto.LastName , ImageUrl = string.Empty};
                var result = await _userManager.CreateAsync(user, userPostDto.Password);
                if(userPostDto.Password != userPostDto.ConfirmPassword)                                                                                                                                                                                                                                                                            
                    ModelState.AddModelError("", "Şifreler uyuşmuyor.");

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}
