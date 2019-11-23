using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace PulsifiCVParser.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public FileUpload FileUpload { get; set; }

        public string Result { get; set; }

        public async Task<IActionResult> OnPostUploadAsync()
        {
            if (!ModelState.IsValid)
            {
                Result = "Please correct the form.";

                return Page();
            }

            // Check the content length in case the file's only
            // content was a BOM and the content is actually
            // empty after removing the BOM.
            if (FileUpload.FormFile.Length == 0)
            {
                ModelState.AddModelError(FileUpload.FormFile.FileName,
                    $"{FileUpload.FormFile.FileName} is empty.");
            }

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await FileUpload.FormFile.CopyToAsync(ms);             
                fileBytes = ms.ToArray();             
            }

            var ext = System.IO.Path.GetExtension(FileUpload.FormFile.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !(ext == ".docx" || ext == ".pdf"))
            {
                ModelState.AddModelError(FileUpload.FormFile.FileName,
                    $"{FileUpload.FormFile.FileName} has non-supported type.");
            }
            if (!ModelState.IsValid)
            {
                Result = "Please correct the form.";

                return Page();
            }
            StringBuilder documentText = new StringBuilder();
            switch (ext)
            {
                case ".docx":
                    using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(FileUpload.FormFile.OpenReadStream(), false))
                    {
                        foreach(var p in wordDocument.MainDocumentPart.Document.Body.Elements())
                        {
                            documentText.AppendLine(p.InnerText);
                        }
                    }
                    break;
                case ".pdf":

                    break;
            }
            var result = await Helpers.RecognizeEntities.RunAsync("https://pulsifitextanalytics.cognitiveservices.azure.com",
                "8d4573a4f354460daf953fb2b89ece28",
                documentText.ToString());

            Result = JsonConvert.SerializeObject(result);
            return Page();
        }
    }

    public class FileUpload
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, MinimumLength = 0)]
        public string Note { get; set; }
    }
}
