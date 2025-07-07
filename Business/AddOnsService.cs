using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.FakeForm;
using Database;
using Database.Context;

namespace Business
{
    public class AddOnsService
    {
        EventContext context = new EventContext();
        public Result CreateAddOn(AddOns model)
        {
            bool exists = context.AddOns.Any(x => x.AddOnName == model.AddOnName);
            if (exists)
            {
                return new Result(false, "Add-On already exists");
            }
            context.AddOns.Add(model);
            return new Result().DBcommit(context, "Add-On added successfully", null, model);
        }
        public Result UpdateAddOn(AddOns model)
        {
            bool exists = context.AddOns.Any(x => x.AddOnName == model.AddOnName);
            if (!exists)
            {
                return new Result(false, "This Add-On doesn't exist");
            }
            context.AddOns.Update(model);
            return new Result().DBcommit(context, "Add-On updated successfully", null, model);
        }
        public Result ListAddOns()
        {
            var addOnsList = context.AddOns.ToList();
            if (addOnsList == null || addOnsList.Count == 0)
            {
                return new Result(false, "No Add-Ons found");
            }
            return new Result(true, "Add-Ons found", addOnsList);
        }
        public Result Single(int id)
        {
            AddOns x = context.AddOns.FirstOrDefault(x => x.AddOnId == id);
            if (x == null)
            {
                return new Result(false, "Add-On not found");
            }
            return new Result(true, "Add-On found", x);
        }
    }
}
