using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Business;
using Business.FakeForm;
using CloudinaryDotNet.Actions;
using Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace WebApp.Pages.Account
{
    [AllowAnonymous]
    public class ResetLinkModel : PageModel
    {
        [BindProperty]
        public FakeLoginForm form { get; set; } = new FakeLoginForm();
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            Result result = new TokenService().CreatePasswordResetToken(form.Email);
            if(result.Success==false)
            {
                TempData["Error"] = result.Message;
                return Page();
            }
            Token token = result.Data as Token;
            result = new UserService().ResetInfo(form);
            if (result.Success == false)
            {
                TempData["Error"] = result.Message;
                return Page();
            }
            string name = result.Data as string;
            string resetLink = $"https://localhost:7290/Account/UpdatePassword?email={Uri.EscapeDataString(form.Email)}&token={Uri.EscapeDataString(token.RandomToken)}";
            Result result1 = new EmailService().SendPasswordResetEmail(form.Email,name,resetLink);
            if (result1.Success == true)
            {
                TempData["Success"] = "Password reset link sent to your email.";
                return RedirectToPage("/Account/Login");
            }
            else
            {
                TempData["Error"] = "An error occured";
                return RedirectToPage("/Account/Login");
            }
        }
    }
}
