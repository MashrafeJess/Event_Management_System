using System;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Database;
using Database.ViewModel;
using DinkToPdf;
using DinkToPdf.Contracts;
using RazorLight;

namespace Business
{
    public class InvoiceService
    {
        private readonly IConverter _pdfConverter;

        public InvoiceService()
        {
            _pdfConverter = new SynchronizedConverter(new PdfTools());
        }

        public async Task<Result> RenderInvoiceHtmlAsync(Payment order)
        {
            try
            {
                var engine = new RazorLightEngineBuilder()
                    .UseFileSystemProject("./Views/Invoice") // Make sure this path is correct
                    .UseMemoryCachingProvider()
                    .Build();

                string html = await engine.CompileRenderAsync("InvoiceTemplate.cshtml", order);
                return new Result(true, "HTML rendered successfully", html);
            }
            catch (Exception ex)
            {
                return new Result(false, $"Error rendering invoice HTML: {ex.Message}");
            }
        }

        public Result GeneratePdfFromHtml(string htmlContent)
        {
            try
            {
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = new GlobalSettings
                    {
                        ColorMode = DinkToPdf.ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = DinkToPdf.PaperKind.A4
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            HtmlContent = htmlContent,
                            WebSettings = { DefaultEncoding = "utf-8" }
                        }
                    }
                };

                byte[] pdf = _pdfConverter.Convert(doc);
                return new Result(true, "PDF generated successfully", pdf);
            }
            catch (Exception ex)
            {
                return new Result(false, $"Error generating PDF: {ex.Message}");
            }
        }

        public Result SendInvoiceEmail(string toEmail, byte[] pdfData, string orderId)
        {
            try
            {
                var message = new MailMessage("noreply@yourapp.com", toEmail)
                {
                    Subject = "Your Invoice",
                    Body = "Please find your invoice attached.",
                    IsBodyHtml = true
                };

                message.Attachments.Add(new Attachment(new MemoryStream(pdfData), $"Invoice-{orderId}.pdf"));

                using var smtp = new SmtpClient("smtp.yourserver.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("your-username", "your-password"),
                    EnableSsl = true
                };

                smtp.Send(message);
                return new Result(true, "Invoice email sent successfully");
            }
            catch (Exception ex)
            {
                return new Result(false, $"Error sending invoice email: {ex.Message}");
            }
        }
    }
}
