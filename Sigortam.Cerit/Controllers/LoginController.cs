using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sigortam.Cerit.Common.Dtos.User;

namespace Sigortam.Cerit.Controllers
{
    public class LoginController : Controller
    {
        //private readonly SignInManager<AppUser>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserLoginDto loginDto)
        {
            return View();
        } 
        public IActionResult Login(UserLoginDto loginDto)
        {
            return View();
        }
    }
}
