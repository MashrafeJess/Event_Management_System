using Database;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Business;

namespace WebApp.Pages.EventInfo
{
    [Authorize(Roles = "1,2")]
    public class OffersModel : PageModel
    {
        [BindProperty]
        public Offers model { get; set; } = new();
        public List<Package> packages { get; set; } = new();
        public List<Events> events { get; set; } = new();
        public void OnGet(int? Id = null)
        {
            if (Id != null)
            {
                Result result = new OffersService().Single(Id.Value);
                model = result.Data as Offers;
            }
            
            packages = new PackageService().List().Data as List<Package>;
            events = new EventService().List().Data as List<Events>;
        }
        public IActionResult OnPost()
        {
            model.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Result result = null;
            if (model.OfferId == 0)
            {
                result = new OffersService().AddOffer(model);
            }
            else
            {
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                result = new OffersService().UpdateOffer(model);
            }
            packages = new PackageService().List().Data as List<Package>;
            //sizes = new SizeService().List().Data as List<EventSize>;
            if (result.Success)
                return RedirectToPage("/EventInfo/OffersList");
            else return Page();
        }
    }
}
