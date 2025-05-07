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
    public class PackageListModel : PageModel
    {
        public List<Package_UserInfo> List { get; set; } = new();
        public void OnGet()
        {
            Result results = new PackageService().PackageNewList();
            if (results.Success)
            {
                List = results.Data as List<Package_UserInfo>;
            }
        }
    }
}
