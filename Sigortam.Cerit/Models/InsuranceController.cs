using Microsoft.AspNetCore.Mvc;
using Sigortam.Cerit.Common.Dtos;

namespace Sigortam.Cerit.Models
{
    public class InsuranceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Insurance(InsuranceDto insurance)
        {
            //var services = new Sigortam.Cerit.Core.Services.Insurance.InsuranceService();
            //services.AddOrUpdateInsurance(insurance);
            return View();
        }
    }
}
