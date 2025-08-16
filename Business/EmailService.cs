using System;
using Business;
using Database;
using Database.Context;
using Database.ViewModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Business
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService()
        {
            // Load appsettings.json manually from current directory
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public Result SendMail(UserData user, Payment payment)
        {
            try
            {
                var host = _config["SmtpSettings:Host"];
                var port = int.Parse(_config["SmtpSettings:Port"]);
                var username = _config["SmtpSettings:Username"];
                var password = _config["SmtpSetonintings:Password"];

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

        public Result SendPasswordResetEmail(string toEmail, string userName, string resetLink)
        {
            try
            {
                var host = _config["SmtpSettings:Host"];
                var port = int.Parse(_config["SmtpSettings:Port"]);
                var username = _config["SmtpSettings:Username"];
                var password = _config["SmtpSettings:Password"];

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(username));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "Password Reset Request";

                var body = $@"
                    <h2>Password Reset</h2>
                    <p>Hi {userName},</p>
                    <p>We received a request to reset your password. Click the link below to choose a new one:</p>
                    <p><a href='{resetLink}' target='_blank'>Reset Password</a></p>
                    <p>If you didn’t request this, please ignore this email.</p>
                    <p>Best regards,<br/>Event Management Team</p>";

                email.Body = new TextPart(TextFormat.Html) { Text = body };

                using var smtp = new SmtpClient();
                smtp.Connect(host, port, SecureSocketOptions.StartTls);
                smtp.Authenticate(username, password);
                smtp.Send(email);
                smtp.Disconnect(true);

                return new Result(true, $"Password reset email sent to {toEmail}",null);
            }
            catch (Exception ex)
            {
                return new Result(false, $"Failed to send password reset email: {ex.Message}");
            }
        }
    }
}
