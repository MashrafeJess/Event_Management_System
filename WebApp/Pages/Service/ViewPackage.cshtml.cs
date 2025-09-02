using System.Security.Claims;
using Business;
using Database;
using Database.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
namespace WebApp.Pages.Service
{
    [Authorize(Roles = "3")]
    public class ViewPackageModel : PageModel
    {
        public List<OfferPackageEvent> List = new List<OfferPackageEvent>();
        public void OnGet(int? Id = null)
        {
            if (Id != null)
            {
                var Result = new PackageService().EventPackages(Id.Value);
                if(Result.Success)
                {
                    List = Result.Data as List<OfferPackageEvent>;
                }
            }
        }
    }
}
