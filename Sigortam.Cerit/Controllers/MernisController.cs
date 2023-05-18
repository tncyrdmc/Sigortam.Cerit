using Microsoft.AspNetCore.Mvc;
using Sigortam.Cerit.Common.Dtos;
using Sigortam.Cerit.Models;

namespace Sigortam.Cerit.Controllers
{
    public class MernisController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> IdentityCheck (InsuranceDto userIdentityCheckDto)
        {
            if(userIdentityCheckDto == null)
                return Json(new { Message = "Beklenmeyen bir hata oluştu lütfen tekrar deneyin", Code = ResultType.Failed });

            var client = new MernisService.KPSPublicSoapClient(MernisService.KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            try
            {
                var response = await client.TCKimlikNoDogrulaAsync(Convert.ToInt64(userIdentityCheckDto.User.IdentificationNumber), userIdentityCheckDto.User.Name, userIdentityCheckDto.User.LastName, userIdentityCheckDto.User.BirthYear);
                if (response.Body.TCKimlikNoDogrulaResult)
                {
                    var services = new Sigortam.Cerit.Core.Services.Insurance.InsuranceService();
                    services.AddOrUpdateInsurance(userIdentityCheckDto);
                    return Json(new { Message = "Başarılı bir şekilde kayıt oluşturuldu", Code = ResultType.Succeeded });
                }
                else
                {
                    return Json(new { Message = "Bilgileri kontrol edip tekrar deneyin", Code = ResultType.Failed });
                }
            }
            catch 
            {
                return Json(new { Message = "İnternet bağlantınızı kontrol edin", Code = ResultType.ConnectionFailed });
            }
        }
    }
}
