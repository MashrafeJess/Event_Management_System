using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Database.Context;
using Database.ViewModel;
namespace Business
{
    public class OrderListService
    {
        EventContext context = new EventContext();
        public Result List()
        {
            var list = context.OrderList.ToList();
            return new Result(true, "Success", list);
        }
        public Result Single(string Id)
        {
            var list = context.OrderList.FirstOrDefault(x => x.UserId == Id);
            return new Result(true, "Success", list);
        }
    }
}
