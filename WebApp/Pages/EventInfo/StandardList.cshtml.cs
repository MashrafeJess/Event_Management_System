using System.Security.Claims;
using Business;
using Database;
using Database.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace WebApp.Pages.EventInfo
{
    [Authorize(Roles = "1,2")]
    public class StandardListModel : PageModel
    {
        public List<Standard> List { get; set; } = new();
        public void OnGet()
        {
            Result results = new StandardService().List();
            if (results.Success)
            {
                List = results.Data as List<Standard>;
            }
        }
        public IActionResult OnPostDeleteEvent(int? id)
        {
            Result result = null;

            //if (id != null)
            //{
            //    result = new StandardService().UpdateStandard(standard);
            //}

            if (result != null && result.Success)
            {
                return RedirectToPage("/EventInfo/EventList");
            }
            else
            {
                return Page();
            }
        }

    }
}
