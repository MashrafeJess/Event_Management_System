using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Context;
using Database;
using Microsoft.EntityFrameworkCore;
namespace Business
{
    public class StandardService
    {
        EventContext context = new EventContext();
        public Result AddStandard(Standard model)
        {
            bool exists = context.Standard.Any(x => x.StandardName == model.StandardName);
            if (exists)
            {
                return new Result(false, "Standard already exists");
            }
            context.Standard.Add(model);
            return new Result().DBcommit(context, "Standard added successfully", null, model);
        }
        public Result UpdateStandard(Standard model)
        {
            Standard standard = context.Standard.Where(x => x.StandardId == model.StandardId).FirstOrDefault();
            if (standard == null)
            {
                return new Result(false, "Standard does not exist");
            }
            context.Standard.Update(standard);
            return new Result().DBcommit(context, "Standard updated successfully", null, standard);
        }
        public Result List()
        {
            var standards = context.Standard.ToList();
            if (standards.Count == 0)
            {
                return new Result(false, "No standards found");
            }
            return new Result(true, "Standards found", standards);
        }
        public Result StandardEventImageList()
        {
            var standards = context.Standard.Include(s => s.Events).ThenInclude(e => e.Images).ToList();
            if (standards.Count == 0)
            {
                return new Result(false, "No Events under Standard found");
            }
            return new Result(true, "Standards & Event found", standards);
        }
        public Result Single(int id)
        {
            var standard = context.Standard.FirstOrDefault(x => x.StandardId == id);
            if (standard == null)
            {
                return new Result(false, "Standard not found");
            }
            return new Result(true, "Standard found", standard);
        }
    }
}
