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
        public string LoggedInUser { get; set; }
        
        [BindProperty]
        public MockForm userForm { get; set; } = new();
        public void OnGet(string? Id = null)
        {
            LoggedInUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Id != null)
            {
                Result result = new UserService().Single(Id);
                UserInfo user = result.Data as UserInfo;
                userForm.UserName = user.UserName;
                userForm.Email = user.Email;
                userForm.PhoneNum = user.PhoneNumber;
                userForm.Role = user.Role;
                userForm.UpdatedDate = DateTime.Now;   
                userForm.UpdatedBy = LoggedInUser;
            }
        }
        public IActionResult OnPost()
        {
            LoggedInUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            userForm.CreatedBy = LoggedInUser;
            Result result = new UserService().Registration(userForm);
            if (result.Success)
                return RedirectToPage("/Index");
            else return Page();
        }
    }
}
