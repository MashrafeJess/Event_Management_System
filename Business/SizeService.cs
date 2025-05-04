using Database;
using Database.Context;

namespace Business
{
    public class SizeService
    {
        EventContext context = new EventContext();

        public Result AddSize(EventSize eventSize)
        {
            bool x = context.EventSize.Any(x => x.SizeName == eventSize.SizeName);
            if (x)
            {
                return new Result(false, "Size already exists");
            }

            context.EventSize.Add(eventSize);
            return new Result().DBcommit(context, "Size added successfully", null, eventSize);
        }
        public Result UpdateSize(EventSize eventSize)
        {
            bool x = context.EventSize.Any(x => x.SizeName == eventSize.SizeName);
            if (!x)
            {
                return new Result(false, "This size is not found");
            }

            context.EventSize.Update(eventSize);
            return new Result().DBcommit(context, "Size updated successfully", null, eventSize);
        }
        public Result List()
        {
            var sizes = context.EventSize.ToList();
            if (sizes.Count == 0)
            {
                return new Result(false, "No sizes found");
            }
            return new Result(true, "Sizes found", sizes);
        }
        public Result SizeNewList()
        {
            var sizes = context.EventSize_UserInfo.ToList();
            return new Result(true, "Sizes found", sizes);
        }

        public Result Single (int id)
        {
            var size = context.EventSize.FirstOrDefault(x => x.SizeId == id);
            return new Result(true, "Sizes found", size);
        }
    }
}

