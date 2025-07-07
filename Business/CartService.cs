using Business;
using Database;
using Database.Context;
using Microsoft.EntityFrameworkCore;
public class CartService
{
    EventContext context = new EventContext();

    public Result AddCart(Cart cart)
    {
        bool alreadyExists = context.Cart.Any(x => x.PackageId == cart.PackageId && x.CreatedBy == cart.CreatedBy && x.EventDate==cart.EventDate);
        if (alreadyExists)
        {
            return new Result(false, "You have already added this package to your cart.");
        }
        context.Cart.Add(cart);
        return new Result().DBcommit(context, "Package added to cart successfully", null, cart);
    }
    public Result UpdateCart(Cart cart)
    {
        var existingCart = context.Cart.Include(c => c.Extra).FirstOrDefault(x => x.CartId == cart.CartId);
        if (existingCart == null)
        {
            return new Result(false, "Cart item not found");
        }
        var alreadyExists = existingCart.Extra.Select(x=>x.AddOnId).ToList();
        var incomingAddOnIds = cart.Extra?.Select(e => e.AddOnId).ToList() ?? new();
        var toRemove = existingCart.Extra
            .Where(e => !incomingAddOnIds.Contains(e.AddOnId))
            .ToList();

        foreach (var item in toRemove)
            existingCart.Extra.Remove(item);
        var toAdd = incomingAddOnIds
            .Where(id => !alreadyExists.Contains(id))
            .ToList();

        foreach (var id in toAdd)
        {
            existingCart.Extra.Add(new CartAddOn { AddOnId = id });
        }
        context.Cart.Update(existingCart);
        return new Result().DBcommit(context, "Cart updated successfully", null, existingCart);
    }
    public Result CartList()
    {
        var cartList = context.Cart.ToList();
        if (cartList == null || cartList.Count == 0)
        {
            return new Result(false, "No items in the cart");
        }
        return new Result(true, "Cart items found", cartList);
    }
    public Result Single(int Id)
    {
        var cart = context.Cart.FirstOrDefault(x=>x.CartId == Id);
        if (cart == null)
        {
            return new Result(false, "No items in the cart");
        }
        return new Result(true, "Cart item found", cart);
    }
    public Result CartDelete(Cart cart)
    {
        bool x = context.Cart.Any(x => x.CartId == cart.CartId);
        if (!x)
        {
            return new Result(false, "This cart item is not found");
        }
        context.Cart.Remove(cart);
        return new Result().DBcommit(context, "Cart item deleted successfully", null, cart);
    }
    public Result GetCartById(int id)
    {
        var cart = context.Cart.FirstOrDefault(x => x.CartId == id);
        if (cart == null)
            return new Result(false, "Cart item not found");
        return new Result(true, null, cart);
    }
    public int GetCartCountByUserId(string userId)
    {
        var cartList = context.Cart.Where(x => x.CreatedBy == userId).ToList();
        int k = cartList.Count;
        return k;
    }

}
