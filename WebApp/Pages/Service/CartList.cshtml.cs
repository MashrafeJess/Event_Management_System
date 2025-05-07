using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Database;
using Business;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace WebApp.Pages.Service
{
    public class CartListModel : PageModel
    {
        public List<Cart> List { get; set; }

        public void OnGet()
        {
            Result result = new CartService().CartList();

            List = result.Data as List<Cart>;
        }

        public IActionResult OnPostDone(int id)
        {
            var service = new CartService();
            var prevOrderService = new PrevOrderService();

            // Get cart item by id
            var cartResult = service.GetCartById(id);
            if (!cartResult.Success || cartResult.Data is not Cart cartItem)
                return Page();

            // Create the order from cart item
            var order = new PrevOrders
            {
                EventName = cartItem.EventName,
                SizeName = cartItem.SizeName,
                Price = cartItem.Price,
                Location = cartItem.Location,
                EventDate = cartItem.EventDate ?? DateTime.Now,
                UserId = cartItem.CreatedBy,
                CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            };

            // Save order first
            var orderResult = prevOrderService.AddPrevOrder(order);

            if (orderResult.Success)
            {
                // If successful, delete cart
                var deleteResult = service.CartDelete(cartItem);
                if (deleteResult.Success)
                {
                    TempData["Success"] = "Order placed successfully!";
                    return RedirectToPage(); // Refresh page
                }
                else
                {
                    TempData["Error"] = "Order saved, but cart delete failed.";
                }
            }
            else
            {
                TempData["Error"] = "Order creation failed.";
            }

            return Page(); // Show error
        }

    }
}
