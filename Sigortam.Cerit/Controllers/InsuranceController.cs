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
        #region cash
        private IInsurance _servis;
        private readonly IMemoryCache _memCache;
        const string _insuranceKey = "insuranceList";
        const string _filterKey = "filter";
        #endregion

        #region ctor
        public InsuranceController(IInsurance servis, IMemoryCache memCache)
        {
            _servis = servis;
            _memCache = memCache;

        }
        #endregion

        public IActionResult Index()
        {
            var insurances = new List<InsuranceDto>();

            if (_memCache.TryGetValue(_insuranceKey, out List<InsuranceDto> insuranceMemoryList))
            {
                insurances = insuranceMemoryList.ToList();
            }
            else
            {
                insurances = GetInsurances();
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
            if (_memCache.TryGetValue(_insuranceKey, out List<InsuranceDto> insuranceMemoryList) && !filterDto.IsResetCasheFilter && insuranceMemoryList.Count > 0
                && (!(_memCache.TryGetValue(_filterKey, out FilterDto filterCash)) || filterCash.SearchText == filterDto.SearchText))
            {
                insurances = insuranceMemoryList.ToList();
            }
            else
            {
                insurances = GetInsurances();
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
                insurances = GetInsurances();
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
                //var address1 = workSheet.Cell(1, 1).Address;
                //var address2 = workSheet.Cell(1, 7).Address;
                //IXLRangeAddress rangeAddress = (IXLRangeAddress)workSheet.Range(address1, address2);
                //workSheet.AutoFilter.Range = new IXLRange("A1:G1");
                //var allDataRange = workSheet.RangeUsed();
                //allDataRange.Sort(7,XLSortOrder.Descending);
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
            var cacheExpOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            };

            string filterSort = filterData.FilterSort;
            var filter = (FilterDto)_memCache.Get(_filterKey);

            if (filter != null && filter.FilterSort == filterData.FilterSort)
            {
                if (filterData.FilterSort.Contains("Desc"))
                {
                    filterData.FilterSort = filterData.FilterSort.Replace("Desc", "");
                }
                else
                {
                    filterData.FilterSort = filterData.FilterSort + "Desc";
                }
            }

            var insurances = new List<InsuranceDto>();
            if (_memCache.TryGetValue(_insuranceKey, out List<InsuranceDto> insuranceMemoryList))
            {
                insurances = insuranceMemoryList.ToList();
            }
            else
            {
                insurances = GetInsurances();
            }
            #region OrderSC
            switch (filterData.FilterSort)
            {
                case "userFullName":
                    insurances = insurances.OrderByDescending(x => x.User.FullName).ToList();
                    break;
                case "userFullNameDesc":
                    insurances = insurances.OrderBy(x => x.User.FullName).ToList();
                    break;
                case "startDate":
                    insurances = insurances.OrderByDescending(x => x.InsuranceStartDate).ToList();
                    break;
                case "startDateDesc":
                    insurances = insurances.OrderBy(x => x.InsuranceStartDate).ToList();
                    break;
                case "endDate":
                    insurances = insurances.OrderByDescending(x => x.InsuranceEndDate).ToList();
                    break;
                case "endDateDesc":
                    insurances = insurances.OrderBy(x => x.InsuranceEndDate).ToList();
                    break;
                case "price":
                    insurances = insurances.OrderByDescending(x => x.Price).ToList();
                    break;
                case "priceDesc":
                    insurances = insurances.OrderBy(x => x.Price).ToList();
                    break;
                case "remainingTime":
                    insurances = insurances.OrderByDescending(x => x.RemainingTime).ToList();
                    break;
                case "remainingTimeDesc":
                    insurances = insurances.OrderBy(x => x.RemainingTime).ToList();
                    break;
                case "isActive":
                    insurances = insurances.OrderByDescending(x => x.IsActive).ToList();
                    break;
                case "isActiveDesc":
                    insurances = insurances.OrderBy(x => x.IsActive).ToList();
                    break;
                default:
                    break;
            }
            #endregion

            if (filter != null)
            {
                filter.FilterSort = filterData.FilterSort;
                _memCache.Set(_filterKey, filter, cacheExpOptions);
            }
            else
            {
                _memCache.Set(_filterKey, filterData, cacheExpOptions);
            }
            _memCache.Set(_insuranceKey, insurances, cacheExpOptions);
            return Json(new { redirectToUrl = Url.Action("Index", "Insurance")});
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
