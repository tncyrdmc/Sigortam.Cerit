using Microsoft.AspNetCore.Mvc;
using IronBarCode;
using ImageInfo = Sigortam.Cerit.Common.Dtos.Barcode.ImageInfo;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

namespace Sigortam.Cerit.Controllers
{
    [Authorize]
    public class BarcodeController : Controller
    {
        const string cacheKey = "file";
        public readonly IMemoryCache _memCache;

        #region ctor
        public BarcodeController(IMemoryCache memCache)
        {
            _memCache = memCache;
        }
        #endregion

        [HttpGet]
        public IActionResult Reader()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ImageReader(IFormFile file)
        {
            var cacheExpOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            };
            ImageInfo path = new ImageInfo();
            string[] barcodeArray = new string[3];
            try
            {
                if (file != null)
                {
                    path = await ImageUploadAsync(file);
                    var resultFromFile = BarcodeReader.Read(path.FullPath);
                    var barcode = resultFromFile.FirstOrDefault()?.Text ?? string.Empty;
                    //ViewBag.Barcode = barcode;
                    barcodeArray = barcode.Split('-');

                    //ViewBag.BarcodeUrl = path.FileName;
                    //_memCache.Set(cacheKey, barcodeArray, cacheExpOptions);
                }
                else
                {
                    ViewBag.Barcode = "Barkod Resmi Ekleyiniz!";
                    ViewBag.BarcodeUrl = "";
                }
            }
            catch (Exception)
            {
                ViewBag.Barcode = "Barkod Okunamadı!";
                ViewBag.BarcodeUrl = path.FileName;
                //ViewBag.ImageSize=path.s
            }
            return Json(new { IdentificationNumber = barcodeArray[2] , PlateNumber = barcodeArray[1] , PermitNumber = barcodeArray[0]});
        }
        private async Task<ImageInfo> ImageUploadAsync(IFormFile file)
        {
            #region ImageUpload
            string path = "";
            string newFileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmssfff") + "-" + file.FileName;
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "wwwroot/Upload"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, newFileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return new ImageInfo { FullPath = path + @"\" + newFileName, FileName = "/Upload/" + newFileName };
                }
                else
                {
                    return new ImageInfo();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
            #endregion
        }
        [HttpGet]
        public ActionResult DeleteCache()
        {
            //Remove ile verilen key'e göre bulunan veriyi siliyoruz
            _memCache.Remove(cacheKey);
            return View();
        }
    }
}
