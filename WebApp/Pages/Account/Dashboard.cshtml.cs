using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database.Context;
using Database;// adjust namespace// adjust namespace

namespace WebApp.Pages.Account
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        private readonly EventContext _context;

        public DashboardModel(EventContext context)
        {
            _context = context;
        }

        public UserInfo CurrentUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login");
            }

            CurrentUser = await _context.UserInfo
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return Page();
        }
    }
}