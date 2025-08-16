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
                var x = context.SaveChanges();
                return new Result(true, Message, Data);
            }
            catch (Exception ex)
            {
                // Check if there's an inner exception
                if (ex.InnerException != null)
                {
                    // You can include both the outer exception message and the inner exception details
                    return new Result(false, $"{FailedMessage ?? ex.Message} Inner Exception: {ex.InnerException.Message}", null);
                }
                else
                {
                    // If no inner exception, just return the main exception message
                    return new Result(false, FailedMessage ?? ex.Message, null);
                }
            }
        }
    }
}
