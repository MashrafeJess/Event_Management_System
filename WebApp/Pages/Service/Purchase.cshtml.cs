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
        public Cart cart { get; set; } = new Cart();
        [BindProperty]
        public OfferPackageEvent offerDetails { get; set; } = new OfferPackageEvent();

        public void OnGet(int? id = null)
        {
            if(id != null)
            {

                Result result = new OffersService().SingleOfferDetail(id.Value);
                if (result.Success)
                {
                    offerDetails = result.Data as OfferPackageEvent;
                }
            }
        }

        public IActionResult OnPost()
        {
            if(cart.CartId==0)
            {
                cart = new Cart
                {
                    EventId = offerDetails.EventId,
                    PackageId = offerDetails.PackageId,
                    OfferId = offerDetails.OfferId,
                    Price = offerDetails.OfferPrice,
                    Location = cart.Location,
                    EventDate = cart.EventDate,
                    CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                };
                Result result = new CartService().AddCart(cart);
                if (result.Success)
                {
                    return RedirectToPage("/Service/CartList");
                }
                else
                {
                    TempData["Error"] = result.Message;
                    return RedirectToPage("/Service/Purchase");
                }
            }
            else
            {
                cart.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                cart.UpdatedDate = DateTime.Now;
                Result result = new CartService().UpdateCart(cart);
                if(!result.Success)
                {
                    return Page();
                }
                return RedirectToPage("/Service/Payment");
            }
        }
    }
}
