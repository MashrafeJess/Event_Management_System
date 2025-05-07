using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Database.ViewModel;
using Business;

namespace WebApp.Pages.Service
{
    [Authorize(Roles = "3")]
    public class PurchaseTypeListModel : PageModel
    {
        [BindProperty]
        public List<Package_UserInfo> List { get; set; } = new();
        public void OnGet(int? Id = null)
        {
            if(Id != null)
            {
                Result result = new PackageService().Multiple(Id.Value);
                List = result.Data as List<Package_UserInfo>;
            }
        }
    }
}
