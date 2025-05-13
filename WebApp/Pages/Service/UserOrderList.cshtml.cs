using System.Security.Claims;
using Business;
using Database;
using Database.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Service
{
    [Authorize(Roles = "3")]
    public class UserOrderListModel : PageModel
    {
        [BindProperty]
        public List<OrderList> List { get; set; } = new();

        public void OnGet(DateTime? startDate, DateTime? endDate)
        {
            string Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Result result;

            if (startDate.HasValue && endDate.HasValue)
            {
                result = new OrderListService().SpecificUserDate(Id, startDate.Value, endDate.Value);
            }
            else
            {
                result = new OrderListService().Single(Id);
            }

            if (result != null && result.Success)
            {
                List = result.Data as List<OrderList>;
            }
        }
    }
}
