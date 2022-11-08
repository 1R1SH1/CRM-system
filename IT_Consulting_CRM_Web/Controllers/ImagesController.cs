using IT_Consulting_CRM_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImagesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult AddImage()
        {
            Image image = new();
            var displayimg = Path.Combine(_webHostEnvironment.WebRootPath, "img");
            DirectoryInfo directoryInfo = new DirectoryInfo(displayimg);
            FileInfo[] filesInfo = directoryInfo.GetFiles();
            image.FileImage = filesInfo;
            return View(image);
        }
        [HttpPost]
        public async Task<IActionResult> AddImage(IFormFile imgFile)
        {
            string ext = Path.GetExtension(imgFile.FileName);
            if (ext == ".jpg" || ext == ".png" || ext == ".gif")
            {
                var imgSave = Path.Combine(_webHostEnvironment.WebRootPath, "img", imgFile.FileName);
                var stream = new FileStream(imgSave, FileMode.Create);
                await imgFile.CopyToAsync(stream);
                stream.Close();
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Delete(string imgDel)
        {
            imgDel = Path.Combine(_webHostEnvironment.WebRootPath, "img", imgDel);
            FileInfo fileInfo = new FileInfo(imgDel);
            if (fileInfo != null)
            {
                System.IO.File.Delete(imgDel);
                fileInfo.Delete();
            }
            return RedirectToAction("AddImage", "Images");
        }
    }
}
