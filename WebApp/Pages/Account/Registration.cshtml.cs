using Business;
using Business.FakeForm;
using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Account
{
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
                return RedirectToPage("/Index");
            else return Page();
        }
    }
}
