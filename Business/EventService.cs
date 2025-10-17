using Database;
using Database.Context;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
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
        public Result UpdateEvent(Events eventModel)
        {
            Events existingEvent = context.Events.FirstOrDefault(x => x.EventId == eventModel.EventId);
            if (existingEvent == null)
            {
                return new Result(false, "Event not found");
            }

            // Detach the existing tracked entity to avoid conflicts
            context.Entry(existingEvent).State = EntityState.Detached;
            if (string.IsNullOrEmpty(eventModel.EventName))
            {
                eventModel.EventName = existingEvent.EventName;
            }
            if (eventModel.StandardId == 0)
            {
                eventModel.EventName = existingEvent.EventName;
            }
            if (string.IsNullOrEmpty(eventModel.CreatedBy))
            {
                eventModel.CreatedBy = existingEvent.CreatedBy;
            }
            // Now, attach the new model and set its state
            context.Events.Update(eventModel);
            return new Result().DBcommit(context, "Event added successfully", null, eventModel);
        }
        public Result List()
        {
            var events = context.Events.ToList();
            if (events.Count == 0)
            {
                return new Result(false, "No event found");
            }
            return new Result(true, "Events found", events);
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
            return new Result(true, "Events found", model);
        }
        public Result EventDelete(int id)
        {
            var model = context.Events.Where(x => x.EventId == id).FirstOrDefault();
            if (model == null)
            {
                return new Result(false, "This event is not found");
            }
            context.Remove(model);
            return new Result().DBcommit(context, "This event was deleted successfully", null);
        }

        public Result AllEventsNameOnly()
        {
            var model = context.Events
                .Select(x => new Events
                {
                    EventId = x.EventId,
                    EventName = x.EventName
                })
                .ToList();

            if (!model.Any())
            {
                return new Result(false, "No events found", null);
            }

            return new Result(true, "Successfully retrieved all names", model);
        }
    }

}
