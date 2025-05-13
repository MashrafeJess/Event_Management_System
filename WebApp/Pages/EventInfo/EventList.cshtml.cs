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
    public class EventListModel : PageModel
    {
        public List<Event_UserInfo> List { get; set; } = new();
        public void OnGet()
        {
            Result results = new EventService().ListWithCreators();
            if (results.Success)
            {
                List = results.Data as List<Event_UserInfo>;
            }
        }
        public IActionResult OnPostDeleteEvent(int? id)
        {
            Result result = null;

            if (id != null)
            {
                result = new EventService().EventDelete(id.Value);
            }

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
