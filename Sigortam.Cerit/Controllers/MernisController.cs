using Microsoft.AspNetCore.Mvc;

namespace Sigortam.Cerit.Controllers
{
    public class MernisController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async void IdentityCheck()
        {
            var client = new MernisService.KPSPublicSoapClient(MernisService.KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            try
            {
                var response = await client.TCKimlikNoDogrulaAsync(Convert.ToInt64(13991590581), "MUSTAFA", "ÖZCERİT", 1997);
                var result = response.Body.TCKimlikNoDogrulaResult;
            }
            catch (Exception ex)
            {
                Exception.Equals(ex.Message, "İnternet bağlantınızı kontrol edin");
            }
            var aaa = 5;
            //return result;
        }
    }
}
