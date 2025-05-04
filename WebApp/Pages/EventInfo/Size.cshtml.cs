using Business;
using Database;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages.EventInfo
{
    [Authorize(Roles = "1,2")]
    public class SizeModel : PageModel
    {
        [BindProperty]
        public EventSize model { get; set; } = new();
        public void OnGet(int? Id = null)
        {
            if (Id != null)
            {
                Result result = new SizeService().Single(Id.Value);
                model = result.Data as EventSize;
            }
        }
        public IActionResult OnPost()
        {
            model.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Result result = null;
            if (model.SizeId == 0)
            {
                result = new SizeService().AddSize(model);
            }
            else
            {
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                result = new SizeService().UpdateSize(model);
            }
            if (result.Success)
                return RedirectToPage("/EventInfo/SizeList");
            else return Page();
        }
    }
}
