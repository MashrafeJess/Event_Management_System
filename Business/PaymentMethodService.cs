using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Context;
using Database;
namespace Business
{
    public class PaymentMethodService
    {
        EventContext context = new EventContext();
        
        public Result AddPaymentMethod(PaymentMethod model)
        {
            bool x = context.PaymentMethod.Any(x => x.PaymentMethodName == model.PaymentMethodName);
            if (x)
            {
                return new Result(false, "Payment method already exists");
            }
            context.PaymentMethod.Add(model);
            return new Result().DBcommit(context, "Payment method added successfully", null, model);
        }
        public Result UpdatePaymentMethod(PaymentMethod model)
        {
            bool exists = context.PaymentMethod.Any(x => x.PaymentMethodId == model.PaymentMethodId);
            if (!exists)
            {
                return new Result(false, "Such payment method not found");
            }
            context.PaymentMethod.Update(model);
            return new Result().DBcommit(context, "Payment method updated successfully", null, model);
        }
        public Result DeactivatePaymentMethod(int id)
        {
            var paymentMethod = context.PaymentMethod.FirstOrDefault(x => x.PaymentMethodId == id);
            if (paymentMethod == null)
            {
                return new Result(false, "Payment method not found");
            }
            paymentMethod.IsPayable = false;
            context.PaymentMethod.Update(paymentMethod);
            return new Result().DBcommit(context, "Payment method deactivated successfully", null, paymentMethod);
        }
        public Result List()
        {
            var paymentMethods = context.PaymentMethod.ToList();
            if (paymentMethods.Count == 0)
            {
                return new Result(false, "No payment methods found");
            }
            return new Result(true, "Payment methods found", paymentMethods);
        }
        public Result Single(int id)
        {
            var paymentMethod = context.PaymentMethod.FirstOrDefault(x => x.PaymentMethodId == id);
            if (paymentMethod == null)
            {
                return new Result(false, "Payment method not found");
            }
            return new Result(true, "Payment method found", paymentMethod);
        }
    }
}
