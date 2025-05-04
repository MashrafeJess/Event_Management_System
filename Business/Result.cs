using Database.Context;

namespace Business
{
    public class Result
    {
        public bool Success;
        public string Message;
        public object? Data;
        public Result() 
        { }
        public Result(bool Success, string Message, object? Data = null)
        {
            this.Success = Success;
            this.Message = Message;
            this.Data = Data;
        }
        public Result DBcommit(EventContext context, string Message, string? FailedMessage = null, object? Data = null)
        {
            try
            {
                context.SaveChanges();
                return new Result(true, Message, Data);
            }
            catch (Exception ex)
            {
                return new Result(false, FailedMessage ?? ex.Message, null);
            }
        }
    }
}
