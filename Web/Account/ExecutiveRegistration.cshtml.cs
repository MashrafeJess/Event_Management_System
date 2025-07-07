using Business.FakeForm;
using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Database;
using System.Security.Claims;
namespace WebApp.Pages.Account
{
    [Authorize(Roles ="1")]
    public class ExecutiveRegistration_Model : PageModel
    {
        [BindProperty]
        public MockForm userForm { get; set; } = new();
        public string LoggedInUser { get; set; }
        public Result result = new Result();

        // For dynamic UI changes
        public string PageTitle { get; set; }
        public string ButtonText { get; set; }
        public bool IsUpdating { get; set; }

        public void OnGet(string? Id = null)
        {
            LoggedInUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Id != null)  // Update scenario
            {
                result = new UserService().Single(Id);
                UserInfo user = result.Data as UserInfo;
                userForm.UserName = user.UserName;
                userForm.Email = user.Email;
                userForm.PhoneNum = user.PhoneNumber;
                userForm.Role = user.Role;
                IsUpdating = true;
                PageTitle = "Update User Info";
                ButtonText = "Update";
            }
            else  // Registration scenario
            {
                IsUpdating = false;
                PageTitle = "Register";
                ButtonText = "Register";
            }
        }

        public IActionResult OnPost()
        {
            LoggedInUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            UserInfo user = result.Data as UserInfo;

            if (IsUpdating)
            {
                userForm.UpdatedBy = LoggedInUser;
                result = new UserService().Update(userForm);  // Update existing user
            }
            else
            {
                userForm.CreatedBy = LoggedInUser;
                result = new UserService().Registration(userForm);  // Register new user
            }

            if (result.Success)
                return RedirectToPage("/Index");
            else
                return Page();
        }
    }
}
