using Business;
using Database;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages.EventInfo
{
    [Authorize(Roles = "1,2")]
    public class MaxOrderLimitModel : PageModel
    {
        [BindProperty]
        public MaxOrderLimit Limit { get; set; } = new();

        public void OnGet(int? id = null)
        {
            if (id != null)
            {
                var result = new MaxOrderLimitService().GetLimit();
                if (result.Success && result.Data is MaxOrderLimit fetchedLimit)
                {
                    Limit = fetchedLimit;
                }
            }
        }

        public IActionResult OnPost()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Result result;

            if (Limit.MaxOrderId == 0)
            {
                Limit.CreatedBy = userId;
                result = new MaxOrderLimitService().AddLimit(Limit);
            }
            else
            {
                Limit.UpdatedBy = userId;
                Limit.UpdatedDate = DateTime.Now;
                result = new MaxOrderLimitService().UpdateLimit(Limit);
            }

            if (result.Success)
            {
                TempData["Message"] = "Max order limit saved successfully!";
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
