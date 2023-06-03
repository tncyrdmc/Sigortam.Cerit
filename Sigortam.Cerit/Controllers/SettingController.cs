using Microsoft.AspNetCore.Mvc;
using Sigortam.Cerit.Common.Dtos.Setting;

namespace Sigortam.Cerit.Controllers
{
    public class SettingController : Controller
    {
        public IActionResult Index()
        {
            //var insurancesCompany = _servis.GetInsuranceCompanys().ToList();
            var settingDto = new SettingDto { RemainingTimeType = RemainingTime.YearMonthDay }; 
            return View(settingDto);
        }
    }
}
