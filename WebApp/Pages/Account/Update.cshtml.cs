using System.Security.Claims;
using Business;
using Business.FakeForm;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace WebApp.Pages.Account
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public MockForm userForm { get; set; } = new MockForm();

        public string LoggedInUser { get; set; }

        public void OnGet(string? Id = null)
        {
            LoggedInUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (LoggedInUser != null)
            {
                Result result = new UserService().Single(LoggedInUser);
                UserInfo user = result.Data as UserInfo;
                userForm.UserName = user.UserName;
                userForm.Email = user.Email;
                userForm.PhoneNum = user.PhoneNumber;
                userForm.Role = user.Role;
                userForm.Password = user.PasswordHash; // Assuming you want to allow password change
            }
        }

        public IActionResult OnPost()
        {
            LoggedInUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (LoggedInUser != null)
            {
                userForm.UpdatedBy = LoggedInUser;
                Result result = new UserService().Update(userForm);
                if (result.Success)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return Page();
                }
            }
            else 
            {
                return RedirectToPage("/Account/Login");
            }
        }
    }
}
