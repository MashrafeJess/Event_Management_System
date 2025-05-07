using System.Security.Claims;
using Business;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace WebApp.Pages.Service
{
    [Authorize(Roles = "3")]
    public class UserOrderListModel : PageModel
    {
        [BindProperty]
        public List<PrevOrders> List { get; set; } = new();
        public void OnGet()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            Result results = new PrevOrderService().Multiple(userId.Value);
            if (results.Success)
            {
                List = results.Data as List<PrevOrders>;
            }
        }
    }
}
