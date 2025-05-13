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
            var list = context.OrderList.OrderBy(o => o.CreatedDate).ToList();
            return new Result(true, "Success", list);
        }
        public Result Single(string Id)
        {
            var list = context.OrderList.Where(x => x.UserId == Id).ToList();
            return new Result(true, "Success", list);
        }
        public Result SpecificDate(DateTime?Start, DateTime? End)
        {
            var list = context.OrderList.Where(x => x.EventDate >= Start && x.EventDate <= End).ToList();
            return new Result(true, "Success", list);
        }
        public Result SpecificUserDate(string? Id,DateTime? Start, DateTime? End)
        {
            var list = context.OrderList.Where(x =>x.UserId==Id && x.EventDate >= Start && x.EventDate <= End).ToList();
            return new Result(true, "Success", list);
        }
    }
}
