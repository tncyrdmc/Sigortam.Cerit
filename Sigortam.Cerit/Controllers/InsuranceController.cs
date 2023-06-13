using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Sigortam.Cerit.Common.Dtos;
using Sigortam.Cerit.Common.Dtos.Filter;
using Sigortam.Cerit.Core.Interfaces;
using Sigortam.Cerit.Models;
using System.Globalization;

namespace Sigortam.Cerit.Controllers
{
    public class InsuranceController : Controller
    {
        private IInsurance _servis;
        private readonly IMemoryCache _memCache;
        const string _insuranceKey = "insuranceList";
        const string _filterKey = "filter";
        const string _searchFilterKey = "searchFilterKey";


        public InsuranceController(IInsurance servis, IMemoryCache memCache)
        {
            _servis = servis;
            _memCache = memCache;

        }

        public IActionResult Index()
        {
            var insurances = new List<InsuranceDto>();

            if (_memCache.TryGetValue(_insuranceKey, out List<InsuranceDto> insuranceMemoryList))
            {
                insurances = insuranceMemoryList.ToList();
            }
            else
            {
                insurances = _servis.GetInsurances();
            }
            if (_memCache.TryGetValue(_filterKey, out FilterDto filter))
            {
                ViewBag.Filter = filter;
            }

            ViewBag.InsuranceActiveCompany = _servis.GetInsuranceCompanys().Where(x => x.IsActive).ToList();
            ViewBag.InsuranceCompany = _servis.GetInsuranceCompanys().ToList();
            return View(insurances);
        }
        public IActionResult FilterInsurance(FilterDto filterDto)
        {
            var insurances = new List<InsuranceDto>();

            var cacheExpOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            };
            if (_memCache.TryGetValue(_insuranceKey, out List<InsuranceDto> insuranceMemoryList) && !filterDto.IsResetCasheFilter && insuranceMemoryList.Count > 0)
            {
                insurances = insuranceMemoryList.ToList();
            }
            else
            {
                insurances = _servis.GetInsurances();
            }

            if (filterDto.StatusType != StatusType.All)
            {
                bool isActive = filterDto.StatusType == StatusType.Enable ? true : false;
                insurances = insurances.Where(x => x.IsActive == isActive).ToList();
            }
            if (filterDto.InsuranceCompanyId != default)
            {
                insurances = insurances.Where(x => x.InsuranceCompany != null && x.InsuranceCompany.InsuranceCompanyId == filterDto.InsuranceCompanyId).ToList();
            }
            if (!String.IsNullOrEmpty(filterDto.SearchText))
            {
                insurances = insurances.Where(x => x.SearchableText.Contains(filterDto.SearchText.ToLower())).ToList();
            }
            insurances = insurances.OrderBy(x => x.IsActive).ToList();
            _memCache.Set(_filterKey, filterDto, cacheExpOptions);
            _memCache.Set(_insuranceKey, insurances, cacheExpOptions);
            return Json(new { redirectToUrl = Url.Action("Index", "Insurance") });
        }
        public IActionResult ExcelExportInsurance()
        {
            var insurances = new List<InsuranceDto>();

            if (_memCache.TryGetValue(_insuranceKey, out List<InsuranceDto> insuranceMemoryList))
            {
                insurances = insuranceMemoryList.ToList();
            }
            else
            {
                insurances = _servis.GetInsurances();
            }

            using (var workbook = new XLWorkbook())
            {
                var workSheet = workbook.Worksheets.Add("Sigortalarım");
                workSheet.Cell(1, 1).Value = "Şirket";
                workSheet.Cell(1, 2).Value = "Kullanıcı";
                workSheet.Cell(1, 3).Value = "Başlangıç Tarihi";
                workSheet.Cell(1, 4).Value = "Bitiş Tarihi";
                workSheet.Cell(1, 5).Value = "Ücret";
                workSheet.Cell(1, 6).Value = "Kalan Gün";
                workSheet.Cell(1, 7).Value = "Durum";

                workSheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                workSheet.Cell(1, 2).Style.Fill.BackgroundColor = XLColor.LightBlue;
                workSheet.Cell(1, 3).Style.Fill.BackgroundColor = XLColor.LightBlue;
                workSheet.Cell(1, 4).Style.Fill.BackgroundColor = XLColor.LightBlue;
                workSheet.Cell(1, 5).Style.Fill.BackgroundColor = XLColor.LightBlue;
                workSheet.Cell(1, 6).Style.Fill.BackgroundColor = XLColor.LightBlue;
                workSheet.Cell(1, 7).Style.Fill.BackgroundColor = XLColor.LightBlue;

                var insurancesCount = 2;

                foreach (var insurance in insurances)
                {
                    workSheet.Cell(insurancesCount, 1).Value = insurance.InsuranceCompany.Name;
                    workSheet.Cell(insurancesCount, 2).Value = insurance?.User.FullName ?? string.Empty;
                    workSheet.Cell(insurancesCount, 3).Value = insurance.InsuranceStartDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                    workSheet.Cell(insurancesCount, 4).Value = insurance.InsuranceEndDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                    workSheet.Cell(insurancesCount, 5).Value = insurance.Price;
                    workSheet.Cell(insurancesCount, 6).Value = insurance.RemainingTime;
                    workSheet.Cell(insurancesCount, 7).Value = insurance.IsActive ? "Aktif" : "Pasif";
                    insurancesCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sigortalarım_" + DateTime.Now.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + ".xlsx");
                }
            }
        }
        [HttpPost]
        public JsonResult FilterSorting(FilterDto filterData)
        {
            //Servisten çekmek yerine dataları sayfadan güncel halini almak gerekiyor search için sıkıntılı durumlar yaşanmasın.
            var insurances = _servis.GetInsurances();
            if (filterData.FilterSort == "userFullName")
            {
                var insurancesfilter = insurances.OrderByDescending(x => x.User.FullName);
                return Json("userFullName");
            }
            else if (filterData.FilterSort == "userFullNameDesc")
            {
                var insurancesfilter = insurances.OrderBy(x => x.User.FullName);
                return Json("userFullNameDesc");
            }
            else if (filterData.FilterSort == "startDate")
            {
                var insurancesfilter = insurances.OrderByDescending(x => x.InsuranceStartDate);
                return Json("startDate");
            }
            else if (filterData.FilterSort == "startDateDesc")
            {
                var insurancesfilter = insurances.OrderBy(x => x.InsuranceStartDate);
                return Json("startDateDesc");
            }
            else if (filterData.FilterSort == "endDate")
            {
                var insurancesfilter = insurances.OrderByDescending(x => x.InsuranceEndDate);
                return Json("endDate");
            }
            else if (filterData.FilterSort == "endDateDesc")
            {
                var insurancesfilter = insurances.OrderBy(x => x.InsuranceEndDate);
                return Json("endDateDesc");
            }
            else if (filterData.FilterSort == "price")
            {
                var insurancesfilter = insurances.OrderByDescending(x => x.Price);
                return Json("price");
            }
            else if (filterData.FilterSort == "priceDesc")
            {
                var insurancesfilter = insurances.OrderBy(x => x.Price);
                return Json("priceDesc");
            }
            else if (filterData.FilterSort == "remainingTime")
            {
                var insurancesfilter = insurances.OrderByDescending(x => x.RemainingTime);
                return Json("remainingTime");
            }
            else if (filterData.FilterSort == "remainingTimeDesc")
            {
                var insurancesfilter = insurances.OrderBy(x => x.RemainingTime);
                return Json("remainingTimeDesc");
            }
            else if (filterData.FilterSort == "isActive")
            {
                var insurancesfilter = insurances.OrderByDescending(x => x.IsActive);
                return Json("isActive");
            }
            else if (filterData.FilterSort == "isActiveDesc")
            {
                var insurancesfilter = insurances.OrderBy(x => x.IsActive);
                return Json("isActiveDesc");
            }
            return Json(true);
        }
        public JsonResult GetInsuranceInformation(int insuranceId)
        {
            var insurance = _servis.GetInsuranceInformation(insuranceId);
            return Json(new { Insurance = insurance });
        }
        private List<InsuranceDto> GetInsurances()
        {
            return _servis.GetInsurances().OrderBy(x => x.IsActive).ToList();
        }
        public JsonResult AddOrUpdateInsurance(InsuranceDto userIdentityCheckDto)
        {
            _servis.AddOrUpdateInsurance(userIdentityCheckDto);
            return Json(new { Message = "Başarılı bir şekilde kayıt oluşturuldu", Code = ResultType.Succeeded });
        }
    }
}
