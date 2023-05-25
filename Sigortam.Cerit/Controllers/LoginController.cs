using Microsoft.AspNetCore.Mvc;

namespace Sigortam.Cerit.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        } 
        [HttpPost]
        public IActionResult Login(string username)
        {
            return View();
        }
    }
}
