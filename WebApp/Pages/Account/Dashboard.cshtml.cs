using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Database;
using Business;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Pages.Account
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        public string IsLoggedInUser { get; set; }
        public UserInfo CurrentUser { get; set; } 

        public IActionResult OnGet()
        {
            // Get current user ID
            IsLoggedInUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(IsLoggedInUser))
            {
                return RedirectToPage("/Account/Login");
            }

            // Get user details from service
            Result result = new UserService().Single(IsLoggedInUser);

            if (result.Data == null || !result.Success)
            {
                return RedirectToPage("/Account/Unauthorized");
            }

            CurrentUser = result.Data as UserInfo;
            return Page();
        }
    }
}