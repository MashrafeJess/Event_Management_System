using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages.Service
{
    public class BookOfferDirectModel : PageModel
    {
        [BindProperty]
        public Cart cart { get; set; }

        [BindProperty]
        public int? SelectedEventId { get; set; }

        [BindProperty]
        public int? SelectedPackageId { get; set; }

        public SelectList EventsSelectList { get; set; }
        public SelectList PackagesSelectList { get; set; }

        // ✅ Load page
        public void OnGet(int? cartId)
        {
            Console.WriteLine("===== BookOfferDirect OnGet() =====");

            // ✅ Load Events
            var eventResult = new EventService().AllEventsNameOnly();
            if (eventResult.Success && eventResult.Data != null)
            {
                var events = (eventResult.Data as IEnumerable<object>)?.Cast<Events>().ToList() ?? new List<Events>();
                Console.WriteLine($"Events loaded: {events.Count}");
                EventsSelectList = new SelectList(events, "EventId", "EventName");
            }
            else
            {
                Console.WriteLine("⚠ No events found!");
                EventsSelectList = new SelectList(new List<Events>());
            }

            // ✅ Load Packages
            var packageResult = new PackageService().AllPackageNamesOnly();
            if (packageResult.Success && packageResult.Data != null)
            {
                var packages = (packageResult.Data as IEnumerable<object>)?.Cast<Package>().ToList() ?? new List<Package>();
                Console.WriteLine($"Packages loaded: {packages.Count}");
                PackagesSelectList = new SelectList(packages, "PackageId", "PackageName");
            }
            else
            {
                Console.WriteLine("⚠ No packages found!");
                PackagesSelectList = new SelectList(new List<Package>());
            }

            // ✅ Existing Cart (Edit Mode)
            if (cartId != null)
            {
                var result = new CartService().Single(cartId.Value);
                cart = result.Data as Cart;

                if (cart != null)
                {
                    SelectedEventId = cart.EventId;
                    SelectedPackageId = cart.PackageId;
                    Console.WriteLine($"Editing Cart: {cart.CartId}, EventId={cart.EventId}, PackageId={cart.PackageId}");
                }
                else
                {
                    cart = new Cart();
                }
            }
            else
            {
                // ✅ New Cart
                cart = new Cart
                {
                    EventDate = DateTime.Now,
                    Location = ""
                };
                Console.WriteLine("Creating new cart...");
            }
        }

        // ✅ AJAX endpoint to get offers based on EventId + PackageId
        public async Task<JsonResult> OnGetOffers(int eventId, int packageId)
        {
            Console.WriteLine($"Fetching offers for EventId={eventId}, PackageId={packageId}");

            var result = await new OffersService().GetByEventAndPackageAsync(eventId, packageId);

            if (result == null || !result.Success || result.Data == null)
            {
                Console.WriteLine("⚠ No offers found for selection.");
                return new JsonResult(new List<object>());
            }

            var offersList = ((IEnumerable<object>)result.Data)
                .Cast<Offers>()
                .Select(o => new
                {
                    offerId = o.OfferId,
                    offerName = o.OfferName
                });

            return new JsonResult(offersList);
        }

        // ✅ Save Cart (POST)
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model state invalid, reloading dropdowns...");
                ReloadDropdowns();
                return Page();
            }

            Result result;
            if (cart.CartId == 0)
            {
                Console.WriteLine("Adding new cart...");
                result = new CartService().AddCart(cart);
            }
            else
            {
                Console.WriteLine($"Updating existing cart {cart.CartId}...");
                result = new CartService().UpdateCart(cart);
            }

            if (!result.Success)
            {
                Console.WriteLine($"❌ Failed: {result.Message}");
                ModelState.AddModelError(string.Empty, result.Message);
                ReloadDropdowns();
                return Page();
            }

            Console.WriteLine("✅ Cart saved successfully!");
            return RedirectToPage("/Cart/List");
        }

        // ✅ Helper to reload dropdowns on form post errors
        private void ReloadDropdowns()
        {
            var eventResult = new EventService().AllEventsNameOnly();
            if (eventResult.Success && eventResult.Data != null)
            {
                var events = (eventResult.Data as IEnumerable<object>)?.Cast<Events>().ToList() ?? new List<Events>();
                EventsSelectList = new SelectList(events, "EventId", "EventName");
            }

            var packageResult = new PackageService().AllPackageNamesOnly();
            if (packageResult.Success && packageResult.Data != null)
            {
                var packages = (packageResult.Data as IEnumerable<object>)?.Cast<Package>().ToList() ?? new List<Package>();
                PackagesSelectList = new SelectList(packages, "PackageId", "PackageName");
            }
        }
    }
}
