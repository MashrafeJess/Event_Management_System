using Business;
using Business.FakeForm;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Account
{
    [AllowAnonymous]
    public class UpdatePasswordModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public MockForm form { get; set; } = new MockForm();
        [BindProperty(SupportsGet = true)]
        public string email {get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public string token { get; set; } = string.Empty;
        public IActionResult OnGet()
        {
            Result result = new TokenService().ValidateToken(email, token);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToPage("/Account/Login"); // Send to login, not self
            }

            form.Email = email; // Prefill email
            return Page(); // Render the page with form
        }

        public IActionResult OnPost()
        {
            Result result = new TokenService().ValidateToken(email, token);
            if (!result.Success)
            {
                TempData["Error"] = "Token is not validated";
                return RedirectToPage("/Account/Login");
            }

            form.Email = email;

            Result result1 = new UserService().Update(form);
            if (!result1.Success)
            {
                TempData["Error"] = "User data couldn't be updated";
                return RedirectToPage("/Account/Login"); // Don't redirect
            }

            result = new TokenService().MarkTokenAsUsed(email, token);
            if (!result.Success)
            {
                TempData["Error"] = "Token couldn't be marked as used";
                return RedirectToPage("/Account/Login");
            }

            TempData["Success"] = "Password updated successfully. You can now log in.";
            return RedirectToPage("/Account/Login");
        }
    }
}