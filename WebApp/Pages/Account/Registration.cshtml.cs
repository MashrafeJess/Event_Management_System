using Business;
using Business.FakeForm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
namespace WebApp.Pages.Account
{
    [AllowAnonymous]
    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public MockForm userForm { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            Result result = new UserService().Registration(userForm);
            if (result.Success)
                return RedirectToPage("/Account/Login");
            else return Page();
        }
    }
}
