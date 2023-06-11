using Microsoft.AspNetCore.Mvc;
using Sigortam.Cerit.Common.Dtos.Barcode;
using System.DrawingCore;
//using ZXing;
//using ZXing.ZKWeb;
using System.Reflection.PortableExecutable;
using IronBarCode;
using ImageInfo = Sigortam.Cerit.Common.Dtos.Barcode.ImageInfo;

namespace Sigortam.Cerit.Controllers
{
    public class BarcodeController : Controller
    {
        [HttpGet]
        public IActionResult Reader()
        {
            return View();
        }

        [HttpPost]
        public async void ImageReader(IFormFile file)
        {
            ImageInfo path = new ImageInfo();
            try
            {
                if (file != null)
                {
                    path = await ImageUploadAsync(file);
                    var resultFromFile = BarcodeReader.Read(path.FullPath);
                    var barcode = resultFromFile.FirstOrDefault()?.Text ?? string.Empty;
                    ViewBag.Barcode = barcode;
                    string [] barcodeArray = barcode.Split('-');

                    ViewBag.BarcodeUrl = path.FileName;
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
            //return View();
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
    }
}
