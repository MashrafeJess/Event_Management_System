using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using Database;
using Database.ViewModel;
using Business;
using Microsoft.AspNetCore.Authorization;
namespace WebApp.Pages.EventInfo
{
    [Authorize(Roles = "1,2")]
    public class AllOrderListModel : PageModel
    {
        [BindProperty]
        public List<PrevOrders> List { get; set; } = new();
        public void OnGet()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            Result results = new PrevOrderService().List();
            if (results.Success)
            {
                List = results.Data as List<PrevOrders>;
            }
        }
    }
}
