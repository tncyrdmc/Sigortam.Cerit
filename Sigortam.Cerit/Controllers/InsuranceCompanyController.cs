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
            //var insurancesCompany = new List<InsuranceCompanyDto>();
            //insurancesCompany.Add(new InsuranceCompanyDto { InsuranceCompanyId = 1, Name = "AKSigorta AŞ", IsActive = true, ImageSvgUrl = "https://cdn.freebiesupply.com/logos/large/2x/aksigorta-logo-png-transparent.png" });
            //insurancesCompany.Add(new InsuranceCompanyDto { InsuranceCompanyId = 2, Name = "Allianz Sigorta AŞ", IsActive = true, ImageSvgUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4b/Allianz.svg" });
            //insurancesCompany.Add(new InsuranceCompanyDto { InsuranceCompanyId = 3, Name = "Ana Sigorta Anonim Şirketi", IsActive = false, ImageSvgUrl = "https://www.anasigorta.com.tr/assets/images/logo/for-white-background.png" });
            //insurancesCompany.Add(new InsuranceCompanyDto { InsuranceCompanyId = 4, Name = "Anadolu Anonim Türk Sigorta Şirketi", IsActive = true, ImageSvgUrl = "https://upload.wikimedia.org/wikipedia/commons/9/9e/Anadolu_Sigorta_logo.svg" });
            //insurancesCompany.Add(new InsuranceCompanyDto { InsuranceCompanyId = 5, Name = "Axa Sigorta AŞ", IsActive = true, ImageSvgUrl = "https://upload.wikimedia.org/wikipedia/commons/9/94/AXA_Logo.svg" });
            //insurancesCompany.Add(new InsuranceCompanyDto { InsuranceCompanyId = 6, Name = "Türkiye Katılım Sigorta A.Ş.", IsActive = true, ImageSvgUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8f/Türkiye_Sigorta_logo.svg" });
            //insurancesCompany.Add(new InsuranceCompanyDto { InsuranceCompanyId = 7, Name = "Şeker Sigorta AŞ", IsActive = true, ImageSvgUrl = "https://www.pratikotomotiv.com/Upload/seker_sigorta-1252020_134644.jpg" });
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
