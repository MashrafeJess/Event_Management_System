using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Database.Context;
namespace Business
{
    public class OrderStatusService
    {
        EventContext context = new EventContext();
        public Result AddOrderStatus(OrderStatus orderStatus)
        {
            bool x = context.OrderStatus.Any(x => x.OrderStatusName == orderStatus.OrderStatusName);
            if(x)
            {
                return new Result(false, "This order status already exists");
            }
            context.OrderStatus.Add(orderStatus);
            return new Result().DBcommit(context, "Order status added successfully", null, orderStatus);
        }
        public Result UpdateOrderStatus(OrderStatus orderStatus)
        {
            bool x = context.OrderStatus.Any(x => x.OrderStatusName == orderStatus.OrderStatusName);
            if (!x)
            {
                return new Result(false, "This order status not found");
            }
            context.OrderStatus.Add(orderStatus);
            return new Result().DBcommit(context, "Order status updated successfully", null, orderStatus);
        }
        public Result ListOrderStatus()
        {
            var orderStatusList = context.OrderStatus.ToList();
            if (orderStatusList.Count == 0)
            {
                return new Result(false, "No order statuses found");
            }
            return new Result(true, "Order statuses found", orderStatusList);
        }
        public Result SingleOrderStatus(int id)
        {
            var orderStatus = context.OrderStatus.FirstOrDefault(x => x.OrderStatusId == id);
            if (orderStatus == null)
            {
                return new Result(false, "Order status not found");
            }
            return new Result(true, "Order status found", orderStatus);
        }
    }
}
