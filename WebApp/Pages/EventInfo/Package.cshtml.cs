using Database;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Business;

namespace WebApp.Pages.EventInfo
{
    [Authorize(Roles = "1,2")]
    public class PackageModel : PageModel
    {
        [BindProperty]
        public Package model { get; set; } = new();
        public List<Events> events { get; set; } = new();
        public List<EventSize> sizes { get; set; } = new();
        public void OnGet(int? Id = null)
        {
            if (Id != null)
            {
                Result result = new PackageService().Single(Id.Value);
                model = result.Data as Package;
            }
            events = new EventService().List().Data as List<Events>;
            sizes = new SizeService().List().Data as List<EventSize>;
        }
        public IActionResult OnPost()
        {
            model.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Result result = null;
            if (model.PackageId == 0)
            {
                result = new PackageService().AddPackage(model);
            }
            else
            {
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                result = new PackageService().UpdatePackage(model);
            }
            events = new EventService().List().Data as List<Events>;
            sizes = new SizeService().List().Data as List<EventSize>;
            if (result.Success)
                return RedirectToPage("/EventInfo/PackageList");
            else return Page();
        }
    }
}
