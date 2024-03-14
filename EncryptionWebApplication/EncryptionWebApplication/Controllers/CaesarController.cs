using EncryptionWebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionWebApplication.Controllers
{
    public class CaesarController : Controller
    {
        public static Dictionary<char, int> frequencyTable;

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var uploadsFolder = Path.Combine("..", Directory.GetCurrentDirectory(), "Files");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = file.FileName;
                    var file_path = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(file_path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    return RedirectToAction("EncryptionWindowSettings", "Caesar", new { FilePath = file_path });
                }
                else return NotFound("Error. There is no uploaded file");

            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

		public IActionResult DownloadFile(string sourceText)
		{
			string fileName = "file.txt";
			string filePath = Path.Combine("..", Directory.GetCurrentDirectory(), fileName);
			System.IO.File.WriteAllText(filePath, sourceText);

			return PhysicalFile(filePath, "application/octet-stream", fileName);
		}


		public IActionResult EncryptionWindowSettings(string FilePath)
        {
            ViewBag.Content = FileService.ReadTextFromFileFile(FilePath);
            return View("EncryptionWindowSettings");

        }

        public IActionResult Attack(string sourceText, string encryptedText)
        {

            string? resul  = CaesarEncryptionService.Attack(sourceText, encryptedText);

            if (resul == null) return null;
            else return Content(resul);

        }

        public IActionResult Encrypt(string sourceText, int step)
        {
            ViewBag.sourceText = sourceText;
            ViewBag.ToReturn = CaesarEncryptionService.Encrypt(sourceText, step);
           

            return View("Encrypt");
        }

        public IActionResult Decrypt(string sourceText, int step)
        {

            ViewBag.ToReturn = CaesarEncryptionService.Decrypt(sourceText, step);
            ViewBag.sourceText = sourceText;


            return View("Decrypt");
        }

        public IActionResult PrintFrequencyTable(string sourceText, int step)
        {
            ViewBag.Encounters = frequencyTable;
            return View("PrintFrequencyTable");

        }

    }
}
