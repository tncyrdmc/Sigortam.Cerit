using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sigortam.Cerit.Core.Interfaces;

namespace Sigortam.Cerit.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        #region cash
        private IInsurance _servis;
        #endregion

        #region ctor
        public DashboardController(IInsurance servis)
        {
            _servis = servis;
        }
        #endregion
        public IActionResult Index()
        {
            var thisMonthInsurances = _servis.GetInsurances().Where(x => x.InsuranceStartDate.Month == DateTime.Now.Month);

            var peopleCount = thisMonthInsurances.Select(x => x.User.UserId).Distinct().Count();

            var insuranceCount = thisMonthInsurances.Count();

            var totalAmount = thisMonthInsurances.Sum(x => x.Price);

            ViewBag.TotalAmount = totalAmount;
            ViewBag.InsuranceCount = insuranceCount;
            ViewBag.PeopleCount = peopleCount;

            return View();
        }
    }
}
