using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sigortam.Cerit.Common.Dtos;
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
            var insurancesCompany = _servis.GetInsuranceCompanys().ToList();
            return View(insurancesCompany);
        }
        [HttpPost]
        public JsonResult UpdateInsuranceCompany([FromBody]List<InsuranceCompanyDto> insuranceCompanyDtos)
        {
           _servis.UpdateInsuranceCompanys(insuranceCompanyDtos);
            return Json(true);
        }
    }
}
