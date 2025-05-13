using Business;
using Database.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
namespace WebApp.Pages.EventInfo  
{
    [Authorize(Roles ="1,2")]
    public class AllOrderListModel : PageModel
    {
        [BindProperty]
        public List<OrderList> List { get; set; } = new();

        public void OnGet(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                Result result = new OrderListService().SpecificDate(startDate.Value, endDate.Value);
                if (result.Success)
                {
                    List = result.Data as List<OrderList>;
                }
            }
            else
            {
                Result result = new OrderListService().List();
                if (result.Success)
                {
                    List = result.Data as List<OrderList>;
                }
            }
        }
    }
}
