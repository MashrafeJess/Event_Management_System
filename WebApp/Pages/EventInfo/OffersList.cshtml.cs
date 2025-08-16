using Business;
using Database;
using Database.ViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages.EventInfo
{
    [Authorize(Roles = "1,2")]
    public class OffersListModel : PageModel
    {
        public List<Offer_Event_Package_User> List { get; set; } = new();
        public void OnGet()
        {
            Result results = new OffersService().ViewList();
            if (results.Success)
            {
                List = results.Data as List<Offer_Event_Package_User>;
            }
        }
    }
}
