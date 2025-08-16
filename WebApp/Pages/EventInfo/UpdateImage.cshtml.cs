using Business;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.EventInfo
{
    [Authorize(Roles = "1,2")]
    public class UpdateImageModel : PageModel
    {
        // Use [FromQuery] to bind the eventId query string explicitly
        [FromQuery]
        public int Id { get; set; }

        [BindProperty]
        public List<Image> ExistingImages { get; set; }

        public Events events = new Events();

        public void OnGet()
        {
            // Debugging: Check if the eventId (Id) is correctly populated
            Console.WriteLine($"EventId from Query: {Id}");  // Check if Id is populated

            if (Id != 0)
            {
                // Retrieve event details based on the eventId
                Result result = new EventService().Single(Id);
                events = result.Data as Events;

                var imageResult = new ImageService().EventImages(Id);
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

        public async Task<IActionResult> OnPostDeleteImageAsync()
        {
            // Retrieve Id from the form
            var imagePath = Request.Form["ImagePath"];
            var imageIdString = Request.Form["ImageId"];
            var eventIdString = Request.Form["Id"];  // Retrieve the Id here

            if (string.IsNullOrEmpty(imagePath) || string.IsNullOrEmpty(imageIdString) || !int.TryParse(imageIdString, out int imageId) || !int.TryParse(eventIdString, out int eventId))
            {
                TempData["Error"] = "Invalid image data.";
                return Page();
            }

            Result result = await new ImageService().RemoveImage(imagePath, imageId);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return Page();
            }

            TempData["Success"] = "Image removed successfully.";
            return RedirectToPage("/EventInfo/UpdateImage", new { Id = eventId });  // Use eventId to redirect
        }
    }
}
