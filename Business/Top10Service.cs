using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Context;
using Database;

namespace Business
{
    public class Top10Service
    {
        EventContext context = new EventContext();
        public Result AddTop10Event(Image img)
        {
            bool x = context.Top10Images.Any(x => x.ImagePath == img.ImagePath || x.ImageId == img.ImageId);
            if (x)
            {
                return new Result(false, "Top 10 event already exists");
            }
            Top10Images top10Images = new Top10Images
            {
                ImageId = img.ImageId,
                ImagePath = img.ImagePath
            };
            context.Top10Images.Add(top10Images);
            return new Result().DBcommit(context, "Top 10 event added successfully", null, img);
        }
        public Result UpdateTop10Event(Top10Images top,Image img)
        {
            var x = context.Top10Images.Any(x => x.Top10ImageId == top.Top10ImageId);
            if (x == null)
            {
                return new Result(false, "Top 10 event not found");
            }
            top.ImageId = img.ImageId;
            top.ImagePath = img.ImagePath;
            context.Top10Images.Update(top);
            return new Result().DBcommit(context, "Top 10 event updated successfully", null, top);
        }
        //public Result ListTop10Events()
        //{
        //    var top10List = context.Top10Images.ToList();
        //    if (top10List.Count == 0)
        //    {
        //        return new Result(false, "No Top 10 events found");
        //    }
        //    return new Result(true, "Top 10 events found", top10List);
        //}
        public Result SingleTop10Event(int id)
        {
            var top10Event = context.Top10Images.FirstOrDefault(x => x.Top10ImageId == id);
            if (top10Event == null)
            {
                return new Result(false, "Top 10 event not found");
            }
            return new Result(true, "Top 10 event found", top10Event);
        }
        public Result DeleteTop10Event(int id)
        {
            Top10Images x = context.Top10Images.Where(x => x.Top10ImageId == id).FirstOrDefault();
            if (x==null)
            {
                return new Result(false, "Top 10 event not found");
            }
            context.Top10Images.Remove(x);
            return new Result().DBcommit(context, "Top 10 event deleted successfully", null, x);
        }
    }
}
