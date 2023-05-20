using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Sigortam.Cerit.Common.Dtos;
using Sigortam.Cerit.Core.Interfaces;
using System.Globalization;

namespace Sigortam.Cerit.Controllers
{
    public class InsuranceController : Controller
    {
        public IInsurance _servis;
        public InsuranceController(IInsurance servis)
        {
            _servis = servis;
        }
        public IActionResult Index()
        {
           var insurances =  _servis.GetInsurances();
            return View(insurances);
        }
        public IActionResult Insurance(InsuranceDto insurance)
        {
            //var services = new Sigortam.Cerit.Core.Services.Insurance.InsuranceService();
            //services.AddOrUpdateInsurance(insurance);
            return View();
        }
        public IActionResult ExcelExportInsurance()
        {
            var insurances = _servis.GetInsurances();
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

                var insurancesCount = 2;

                foreach (var insurance in insurances)
                {
                    workSheet.Cell(insurancesCount, 1).Value = insurancesCount;
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
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sigortalarım_"+DateTime.Now.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)+".xlsx");
                }
            }
        }
    }
}
