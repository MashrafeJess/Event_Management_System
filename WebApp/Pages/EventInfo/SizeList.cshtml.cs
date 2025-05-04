using Business;
using Database;
using Database.ViewModel;
using Database.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.EventInfo
{
    [Authorize(Roles = "1,2")]
    public class SizeListModel : PageModel
    {
        public List<EventSize_UserInfo> List { get; set; } = new();
        public void OnGet()
        {
            Result results = new SizeService().SizeNewList();
            if (results.Success)
            {
                List = results.Data as List<EventSize_UserInfo>;
            }
        }
    }
}
