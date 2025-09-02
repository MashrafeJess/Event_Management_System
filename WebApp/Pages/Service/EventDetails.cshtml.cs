using System.Security.Claims;
using Business;
using Database;
using Database.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace WebApp.Pages.Service
{
    [Authorize(Roles = "3")]
    public class EventDetailsModel : PageModel
    {
        public List<Standard> List = new List<Standard>();
        public void OnGet()
        {
            Result result = new StandardService().StandardEventImageList();
            if (result.Success)
            {
                List = result.Data as List<Standard>;
            }
        }
    }
}
