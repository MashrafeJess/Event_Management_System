using Business;
using Database;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages.EventInfo
{
    [Authorize(Roles = "1,2")]
    public class StandardModel : PageModel
    {
        [BindProperty]
        public Standard? standard { get; set; }
        Result result;
        public void OnGet(int? id = null)
        {
            if (id != null)
            {
                result = new StandardService().Single(id.Value);
                if(result.Success)
                {
                    standard = result.Data as Standard;
                }
            }
        }
        public IActionResult OnPost()
        {
            if (standard.StandardId == 0)
            {
                standard.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                result = new StandardService().AddStandard(standard);
                if (result.Success)
                {
                    TempData["Success"] = result.Message;
                    return RedirectToPage("/EventInfo/StandardList");
                }
                else
                {
                    TempData["Error"] = result.Message;
                    return RedirectToPage("/EventInfo/StandardList");
                }
            }
            else
            {
                standard.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                standard.UpdatedDate = DateTime.Now;
                result = new StandardService().UpdateStandard(standard);
                if(result.Success)
                {
                    TempData["Success"] = result.Message;
                    return RedirectToPage("/EventInfo/StandardList");
                }
                else
                {
                    TempData["Success"] = result.Message;
                    return RedirectToPage("/EventInfo/StandardList");
                }
            }
        }
    }
}
