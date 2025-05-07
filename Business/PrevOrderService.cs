using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Context;
using Database;
using Database.ViewModel;
namespace Business
{
    public class PrevOrderService
    {
        EventContext context = new EventContext();
        public Result AddOrder(PrevOrders order)
        {
            try
            {
                context.PrevOrders.Add(order); // step 1: add

                int saved = context.SaveChanges(); // step 2: save
                if (saved > 0)
                {
                    return new Result(true, "Order saved successfully", order);
                }
                else
                {
                    return new Result(false, "SaveChanges returned 0 rows affected");
                }
            }
            catch (Exception ex)
            {
                return new Result(false, $"Error saving order: {ex.Message}");
            }
        }

        public Result AddPrevOrder(PrevOrders model)
        { 
            context.PrevOrders.Add(model);
            return new Result().DBcommit(context, "Order added successfully", null, model);
        }
        public Result Single (string id)
        {
            var model = context.PrevOrders.FirstOrDefault(x => x.OrderId == id);
            if (model == null)
            {
                return new Result(false, "Order not found");
            }
            return new Result(true, "Order found", model);
        }
        public Result Multiple(string id)
        {
            var model = context.PrevOrders.Where(x => x.UserId == id).ToList();
            if (model == null || model.Count == 0)
            {
                return new Result(false, "No orders found");
            }
            return new Result(true, "Orders found", model);
        }
        public Result List()
        {
            var model = context.PrevOrders.ToList();
            if (model == null || model.Count == 0)
            {
                return new Result(false, "No orders found");
            }
            return new Result(true, "Orders found", model);
        }   
    }
}
