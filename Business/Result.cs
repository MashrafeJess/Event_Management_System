using Database.Context;

namespace Business
{
    public class Result
    {
        public bool Success;
        public string message;
        public object? Data;
        public Result() { }
        public Result(bool Success, string message, object? Data = null)
        {
            this.Success = Success;
            this.message = message;
            this.Data = Data;
        }
        public static Result DBcommit(EventContext context, string message, string FailedMessage = null, object Data = null)
        {
            try
            {
                context.SaveChanges();
                return new Result(true, message, Data);
            }
            catch (Exception ex)
            {
                return new Result(false, FailedMessage ?? ex.Message, null);
            }
        }
    }
}
