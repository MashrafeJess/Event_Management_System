using Business;
using Database;
using Database.Context;

public class CartService
{
    private readonly EventContext context = new EventContext();

    public Result AddCart(Cart cart)
    {
        var result = new MaxOrderLimitService().GetLimit(1);

        if (!result.Success)
        {
            return new Result(false, "Max order limit not found");
        }
        bool x = context.OrderList.Any(x => x.EventDate == cart.EventDate);
        if(x)
        {
            return new Result(false, "This date is already booked");
        }
        MaxOrderLimit limit = result.Data as MaxOrderLimit;

        bool alreadyExists = context.Cart.Any(x => x.PackageId == cart.PackageId && x.CreatedBy == cart.CreatedBy );
        if (alreadyExists)
        {
            return new Result(false, "You have already added this package to your cart.");
        }

        if (limit.MaxOrder <= 0)
        {
            return new Result(false, "Order limit reached. Please try again later.");
        }
        limit.MaxOrder--;
        limit.UpdatedBy = cart.CreatedBy;
        limit.UpdatedDate = DateTime.Now;

        context.MaxOrderLimit.Update(limit);

        context.Cart.Add(cart);

        return new Result().DBcommit(context, "Package added to cart successfully", null, cart);
    }
    public Result UpdateCart(Cart cart)
    {
        var existingCart = context.Cart.FirstOrDefault(x => x.CartId == cart.CartId);
        if (existingCart == null)
        {
            return new Result(false, "Cart item not found");
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
