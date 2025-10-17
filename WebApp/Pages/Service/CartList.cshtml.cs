using System.Collections.Generic;
using System.Linq;
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
    public class CartListModel : PageModel
    {
        public List<JoinCart> cart { get; set; }

        public void OnGet()
        {
            Result result = new CartService().JoinCartList();
            if(result.Success)
            {

                cart = result.Data as List<JoinCart>;
            }

        }

        //public IActionResult OnPostDone(int id)
        //{
        //    var service = new CartService();
        //    var prevOrderService = new PrevOrderService();

        //    Get cart item by id
        //   var cartResult = service.GetCartById(id);
        //    if (!cartResult.Success || cartResult.Data is not Cart cartItem)
        //        return Page();

        //    Create the order from cart item
        //    var order = new PrevOrders
        //    {
        //        EventName = cartItem.EventName,
        //        SizeName = cartItem.SizeName,
        //        Price = cartItem.Price,
        //        Location = cartItem.Location,
        //        EventDate = cartItem.EventDate ?? DateTime.Now,
        //        UserId = cartItem.CreatedBy,
        //        CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        //    };

        //    Save order first
        //   var orderResult = prevOrderService.AddPrevOrder(order);

        //    if (orderResult.Success)
        //    {
        //        // If successful, delete cart
        //        var deleteResult = service.CartDelete(cartItem);
        //        if (deleteResult.Success)
        //        {
        //            TempData["Success"] = "Order placed successfully!";
        //            return RedirectToPage(); // Refresh page
        //        }
        //        else
        //        {
        //            TempData["Error"] = "Order saved, but cart delete failed.";
        //        }
        //    }
        //    else
        //    {
        //        TempData["Error"] = "Order creation failed.";
        //    }

        //    return Page(); // Show error
        //}

    }
}
