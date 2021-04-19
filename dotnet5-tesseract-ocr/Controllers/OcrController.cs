using System;
using System.IO;
using dotnet5_tesseract_ocr.Model;
using Microsoft.AspNetCore.Mvc;
using Tesseract;

namespace dotnet5_tesseract_ocr.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OcrController : ControllerBase
    {
        private const string folderName = "images/";
        private const string trainedDataFolderName = "tessdata";

        [HttpPost]
        public string DoOCR([FromForm] OcrModel request)
        {

            var name = request.Image.FileName;
            var image = request.Image;

            if (image.Length > 0)
            {
                using (var fileStream = new FileStream(folderName + image.FileName, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            var tessPath = Path.Combine(trainedDataFolderName, "");
            string result;

            using (var engine = new TesseractEngine(tessPath, request.DestinationLanguage, EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(folderName + name))
                {
                    var page = engine.Process(img);
                    result = page.GetText();
                    Console.WriteLine(result);
                }
            }
            return string.IsNullOrWhiteSpace(result) ? "Ocr is finished. Return empty" : result;


        }

    }
}
