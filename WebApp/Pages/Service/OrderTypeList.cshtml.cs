using Business;
using Database.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Database;
namespace WebApp.Pages.Service
{
    [Authorize(Roles = "3")]
    public class OrderModel : PageModel
    {
        public List<Events> List { get; set; } = new();
        public void OnGet()
        {
            Result results = new EventService().List();
            if (results.Success)
            {
                List = results.Data as List<Events>;
            }
        }
    }
}
