using Business;
using Database;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages.EventInfo
{
    [Authorize(Roles="1,2")]
    public class EventsModel : PageModel
    {
        [BindProperty]
        public Events model { get; set; } = new();
        public void OnGet(int? Id = null)
        {
            if (Id != null)
            {
                Result result = new EventService().Single(Id.Value);
                model = result.Data as Events;
            }
        }
        public IActionResult OnPost()
        {
            model.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Result result = null;
            if (model.EventId == 0)
            {
                result = new EventService().AddEvent(model);
            }
            else
            {
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                result = new EventService().UpdateEvent(model);
            }
            if (result.Success)
                return RedirectToPage("/EventInfo/EventList");
            else return Page();
        }
    }
}
