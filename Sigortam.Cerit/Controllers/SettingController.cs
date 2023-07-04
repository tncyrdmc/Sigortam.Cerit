using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sigortam.Cerit.Common.Dtos.Setting;
using Sigortam.Cerit.Core.Interfaces;

namespace Sigortam.Cerit.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        private ISetting _servis;

        public SettingController(ISetting servis)
        {
            _servis = servis;
        }

        public IActionResult Index()
        {
            var settingDto = _servis.GetSettings();
            //var settingDto = new SettingDto { RemainingTimeType = RemainingTime.YearMonthDay }; 
            return View(settingDto);
        }
    }
}
