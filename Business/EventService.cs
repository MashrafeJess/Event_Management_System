using Database;
using Database.Context;
namespace Business
{
    public class EventService
    {
        EventContext context = new EventContext();
        public Result AddEvent(Events model)
        {
            bool x = context.Events.Any(x => x.EventName == model.EventName);
            if (x)
            {
                return new Result(false, "Event already exists");
            }
            context.Events.Add(model);
            return new Result().DBcommit(context, "Event added successfully", null, model);
        }
        public Result UpdateEvent(Events model)
        {
            bool x = context.Events.Any(x => x.EventName == model.EventName);
            if (x)
            {
                return new Result(false, "This event is already in use");
            }

            context.Events.Update(model);
            return new Result().DBcommit(context, "Size updated successfully", null, model);
        }
        public Result List()
        {
            var events = context.Events.ToList();
            if (events.Count == 0)
            {
                return new Result(false, "No sizes found");
            }
            return new Result(true, "Sizes found", events);
        }
        public Result ListWithCreators()
        {
            try
            {
                var list = context.Event_UserInfo.ToList();

                return new Result(true, "Success", list);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result Single(int id)
        {
            var model = context.Events.FirstOrDefault(x => x.EventId == id);
            return new Result(true, "Sizes found", model);
        }
        public Result EventDelete(int id)
        {
            var model = context.Events.Where(x => x.EventId == id).FirstOrDefault();
            if(model == null)
            {
                return new Result(false, "This event is not found");
            }
            context.Remove(model);
            return new Result().DBcommit(context, "This event was deleted successfully", null);
        }
    }
}
