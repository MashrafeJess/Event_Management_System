using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using Business;
using Database; 
using Database.Context;
namespace Business
{
    public class PaymentService
    {
        EventContext context = new EventContext();
        public Result AddPayment(Payment payment)
        {
            var pay = new Payment
            {
                StandardId = payment.StandardId,
                PackageId = payment.PackageId,
                OfferId = payment.OfferId,
                Bill = payment.Bill,
                EventDate = payment.EventDate,
                Location = payment.Location,
                PaymentMethodId = payment.PaymentMethodId,
                AddOnIds = new List<PaymentAddOn>()
            };

            if (payment.AddOnIds != null)
            {
                foreach (var id in payment.AddOnIds)
                {
                    payment.AddOnIds.Add(new PaymentAddOn
                    {
                        AddOnId = id.AddOnId
                    });
                }
            }
            context.Payment.Add(payment);
            return new Result().DBcommit(context, "Payment added successfully", null, payment);
        }
        public Result UpdatePayment(Payment model)
        {
            bool x = context.Payment.Any(x=>x.PaymentId == model.PaymentId);
            if(!x)
            {
                return new Result(false,"Such payment model not found");
            }
            context.Payment.Update(model);
            return new Result().DBcommit(context,"Payment updated successfully", null,model);
        }
        public Result List()
        {
            var paymentList = context.Payment.ToList();
            if (paymentList.Count == 0)
            {
                return new Result(false, "No payments found");
            }
            return new Result(true, "Payments found", paymentList);
        }
        public Result Single(string id)
        {
            var payment = context.Payment.FirstOrDefault(x => x.PaymentId == id);
            if (payment == null)
            {
                return new Result(false, "Payment not found");
            }
            return new Result(true, "Payment found", payment);
        }
        public Result PaymentCancel(string id) // Order Status Change (0 means cancelled)
        {
            var payment = context.Payment.FirstOrDefault(x => x.PaymentId == id);
            if (payment == null)
            {
                return new Result(false, "Payment not found");
            }
            payment.OrderStatusId = 0;
            context.Payment.Update(payment);
            return new Result().DBcommit(context, "Payment cancelled successfully", null, payment);
        }
        public Result PaymentComplete(string id) // Order Status Change (2 means completed)
        {
            var payment = context.Payment.FirstOrDefault(x => x.PaymentId == id);
            if (payment == null)
            {
                return new Result(false, "Payment not found");
            }
            payment.OrderStatusId = 2;
            context.Payment.Update(payment);
            return new Result().DBcommit(context, "Order completed successfully", null, payment);
        }
        //public class NotificationService
        //{
        //    private readonly EventContext context;
        //    private readonly EmailService emailService;

        //    public NotificationService(EventContext context, EmailService emailService)
        //    {
        //        this.context = context;
        //        this.emailService = emailService;
        //    }

        //    public Result Notification()
        //    {
        //        var expiredOrders = context.Payment
        //            .Where(c => c.EventDate < DateTime.Now && !c.IsNotified && c.OrderStatusId == 1)
        //            .ToList();

        //        foreach (var order in expiredOrders)
        //        {
        //            var adminsAndManagers = context.UserData
        //                .Where(u => u.RoleName == "Admin" || u.RoleName == "Manager")
        //                .ToList();

        //            foreach (var user in adminsAndManagers)
        //            {
        //                emailService.SendMail(user, order);
        //            }

        //            order.IsNotified = true;
        //            context.Payment.Update(order);
        //        }

        //        return new Result().DBcommit(context, "Notification sent successfully", null, null);
        //    }
        //}

    }
}
