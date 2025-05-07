using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Context;
using Database;
using Database.ViewModel;
using Business;
namespace Business
{
    public class MaxOrderLimitService
    {
        EventContext context = new EventContext();
        public Result AddLimit(MaxOrderLimit maxOrderLimit)
        {
            context.MaxOrderLimit.Add(maxOrderLimit);
            return new Result().DBcommit(context, "Max Order Limit added successfully", null, maxOrderLimit);
        }
        public Result UpdateLimit(MaxOrderLimit maxOrderLimit)
        {
            context.MaxOrderLimit.Update(maxOrderLimit);
            return new Result().DBcommit(context, "Max Order Limit added successfully", null, maxOrderLimit);
        }
        public Result GetLimit(int id = 1)
        {
            //Console.WriteLine($"Searching for MaxOrderId: {id}");
            var maxOrderLimit = context.MaxOrderLimit.FirstOrDefault(x => x.MaxOrderId == id);

            if (maxOrderLimit == null)
            {
                return new Result(false, $"No Max Order Limit found with ID {id}");
            }

            return new Result(true, "Found", maxOrderLimit); 

        }

    }
}
