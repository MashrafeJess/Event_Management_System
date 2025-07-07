using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Business;
using Database;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebApp.Pages
{
    public class ImageUploadModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly ImageService _imageService;

        public ImageUploadModel(IWebHostEnvironment env)
        {
            _env = env;
            _imageService = new ImageService(); 
        }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public Result UploadResult { get; set; }
        public string UploadedImageUrl { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Upload == null || Upload.Length == 0)
            {
                UploadResult = new Result(false, "No file selected.");
                return Page();
            }

            // Save image to wwwroot/uploads/
            string uploadDir = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            string fileName = Path.GetFileName(Upload.FileName);
            string filePath = Path.Combine(uploadDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Upload.CopyToAsync(stream);
            }

            // Build public image URL
            UploadedImageUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";

            // Create Image object for DB
            var imageModel = new Image
            {
                ImagePath = UploadedImageUrl,
                ImageHash = Guid.NewGuid().ToString(), // Optional hash logic
                CreatedDate = DateTime.Now
            };

            UploadResult = _imageService.AddImage(imageModel);

            return Page();
        }
    }
}
