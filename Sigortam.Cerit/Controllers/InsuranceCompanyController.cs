using Microsoft.AspNetCore.Mvc;
using Sigortam.Cerit.Core.Interfaces;

namespace Sigortam.Cerit.Controllers
{
    public class InsuranceCompanyController : Controller
    {
        public IInsurance _servis;
        public InsuranceCompanyController(IInsurance servis)
        {
            _servis = servis;
        }
        public IActionResult Index()
        {
            var insurancesCompany = _servis.GetInsuranceCompanys().OrderBy(x => x.IsActive).ToList();
            return View(insurancesCompany);
        }
        public void AddInsuranceCompany()
        {

        }
    }
}
