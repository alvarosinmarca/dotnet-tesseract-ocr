using Microsoft.AspNetCore.Http;

namespace dotnet_tesseract_ocr.Model
{
    public class OcrModel
    {
        public string DestinationLanguage { get; set; }
        public IFormFile Image { get; set; }
    }

}
