using System;
using Business;
using Database;
using Database.ViewModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Business.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public Result SendMail(UserData user, Payment payment)
        {
            try
            {
                // Load SMTP config from appsettings.json
                var host = _config["SmtpSettings:Host"];
                var port = int.Parse(_config["SmtpSettings:Port"]);
                var username = _config["SmtpSettings:Username"];
                var password = _config["SmtpSettings:Password"];

                // Create the email
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(username));
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = "Order Confirmation - Event Management System";

                var body = $@"
                    <h1>Order Confirmation</h1>
                    <p>Dear {user.UserName},</p>
                    <p>Thank you for your order!</p>
                    <p><strong>Order ID:</strong> {payment.PaymentId}</p>
                    <p><strong>Total Amount:</strong> {payment.Bill} BDT</p>
                    <p><strong>Payment Method:</strong> {payment.PaymentMethodId}</p>
                    <p>We appreciate your business!</p>
                    <p>Best regards,<br>Event Management Team</p>";

                email.Body = new TextPart(TextFormat.Html) { Text = body };

                // Send the email
                using var smtp = new SmtpClient();
                smtp.Connect(host, port, SecureSocketOptions.StartTls);
                smtp.Authenticate(username, password);
                smtp.Send(email);
                smtp.Disconnect(true);

                return new Result(true, $"Email sent successfully to {user.Email}");
            }
            catch (Exception ex)
            {
                return new Result(false, $"Failed to send email: {ex.Message}");
            }
        }
    }
}
