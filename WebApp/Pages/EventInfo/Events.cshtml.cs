using System.Security.Claims;
using System.Security.Cryptography;
using Business;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
namespace WebApp.Pages.EventInfo
{
    [Authorize(Roles = "1,2")] // Image Upload Code is here!!!
    public class EventsModel : PageModel
    {
        public int eventId { get; set; } = new();
        [BindProperty]
        public Events model { get; set; } = new();
        [BindProperty]
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        public List<Image> ExistingImages { get; set; }
        public List<Standard> standards { get; set; } = new List<Standard>();

        public void OnGet(int? Id = null)
        {
            Result StandResult = new StandardService().List();
            standards = StandResult.Data as List<Standard>;
            if (Id != null)
            {
                Result result = new EventService().Single(Id.Value);
                model = result.Data as Events;
                eventId = model.EventId;
                var imageResult = new ImageService().EventImages(model.EventId);
                if (imageResult.Success && imageResult.Data is List<Image> images)
                {
                    ExistingImages = images;
                }
                else
                {
                    ExistingImages = new List<Image>();
                }
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //    return Page();

            Result eventResult;

            if (model.EventId==0 || model.EventId==null) // New event - add
            {
                model.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                eventResult = new EventService().AddEvent(model);
                if (!eventResult.Success)
                {
                    TempData["Error"] = eventResult.Message;
                    return Page();
                }

                var x = eventResult.Data as Events;
                // Use shared method for image processing
                var imageEntities = await ProcessEventImages(x.EventId);

                if (imageEntities.Count > 0)
                {
                    Result addImagesResult = new ImageService().AddImages(imageEntities);
                    if (!addImagesResult.Success)
                    {
                        TempData["Error"] = addImagesResult.Message;
                        return Page();
                    }
                }
                return RedirectToPage("/EventInfo/EventList");
            }
            else // Existing event - update
            {
                // Update the event details first
                var updatedEvent = new Events
                {
                    EventId = model.EventId,
                    EventName = model.EventName,
                    StandardId = model.StandardId,
                    UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UpdatedDate = DateTime.Now
                };

                eventResult = new EventService().UpdateEvent(updatedEvent);
                if (!eventResult.Success)
                {
                    TempData["Error"] = eventResult.Message;
                    return Page();
                }

                // Use shared method for image processing (same logic for both add and update)
                var imageEntities = await ProcessEventImages(model.EventId);

                if (imageEntities.Count > 0)
                {
                    Result addImagesResult = new ImageService().AddImages(imageEntities);
                    if (!addImagesResult.Success)
                    {
                        TempData["Error"] = "Images couldn't be saved because of AddImages while updating an old event";
                        return Page();
                    }
                }
            }

            return RedirectToPage("/EventInfo/EventList");
        }

        // Shared image processing method (for both add and update)
        private async Task<List<Image>> ProcessEventImages(int eventId)
        {
            var imageEntities = new List<Image>();
            string basePath = AppContext.BaseDirectory;
            string folderPath = Path.Combine("wwwroot", "uploads", "events", eventId.ToString());
            Directory.CreateDirectory(folderPath);

            foreach (var formFile in Images)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(formFile.FileName);
                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    // Hash the image file before sending it
                    string fileHash = ComputeFileHash(formFile);

                    imageEntities.Add(new Image
                    {
                        EventId = eventId,
                        ImagePath = Path.Combine("uploads", "events", eventId.ToString(), fileName).Replace("\\", "/"),
                        ImageHash = fileHash,  // Store the file hash
                        CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    });
                }
            }

            return imageEntities;
        }

        // Hash generation method
        public static string ComputeFileHash(IFormFile file, int maxLength = 50)
        {
            using (var sha256 = SHA256.Create())
            using (var stream = file.OpenReadStream())
            {
                byte[] hashBytes = sha256.ComputeHash(stream);

                // Convert the hash to a hexadecimal string
                string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                // Truncate the hash to the maximum length specified (50 characters)
                if (hash.Length > maxLength)
                {
                    hash = hash.Substring(0, maxLength);
                }

                return hash;
            }
        }

    }
}
