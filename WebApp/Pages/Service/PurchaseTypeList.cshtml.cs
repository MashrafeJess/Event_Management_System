using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Database.ViewModel;
using Business;

namespace WebApp.Pages.Service
{
    [Authorize(Roles = "3")]
    public class PurchaseTypeListModel : PageModel
    {
        [BindProperty]
        public List<OfferPackageEvent> List { get; set; } = new();
        public void OnGet(int? Id = null)
        {
            if (Id != null)
            {
                Result result = new OffersService().PackageOffers(Id.Value);
                List = result.Data as List<OfferPackageEvent>;
            }
        }
    }
}
