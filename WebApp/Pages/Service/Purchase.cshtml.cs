using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Database;
using Database.ViewModel;
using Business;

namespace WebApp.Pages.Service
{
    [Authorize(Roles = "3")]
    public class PurchaseModel : PageModel
    {
        [BindProperty]
        public Cart cart { get; set; } = new();

        public Package_UserInfo package { get; set; } = new();

        public IActionResult OnGet(int? id = null)
        {
            if (id == null)
            {
                return RedirectToPage("/Service/OrderTypeList");
            }

            Result result = new PackageService().PackageInfoList(id.Value);
            if (result.Data is Package_UserInfo packageResult)
            {
                package = packageResult;

                cart = new Cart
                {
                    PackageId = package.PackageId
                };
            }
            else
            {
                return RedirectToPage("/Service/OrderTypeList");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                if (cart.PackageId > 0)
                {
                    Result result = new PackageService().PackageInfoList(cart.PackageId);
                    package = result.Data as Package_UserInfo ?? new Package_UserInfo();
                    Console.WriteLine($"PackageId: {cart.PackageId}, EventName: {package.EventName}, SizeName: {package.SizeName}, Price: {package.Price}");
                }
                return Page();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            cart.CreatedBy = userId;

            var packageResult = new PackageService().PackageInfoList(cart.PackageId);
            if (packageResult.Data is Package_UserInfo pkg)
            {
                cart.EventName = pkg.EventName;
                cart.SizeName = pkg.SizeName;
                cart.Price = pkg.Price.Value;

                Console.WriteLine($"Cart EventName: {cart.EventName}, SizeName: {cart.SizeName}, Price: {cart.Price}");
            }

            var cartService = new CartService();
            if (cart.CartId == null || cart.CartId == 0)
            {
                cartService.AddCart(cart);
            }
            else
            {
                cartService.UpdateCart(cart);
            }

            return RedirectToPage("/Service/CartList");
        }
    }
}
