// using SendGrid's C# Library
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Example
{
    internal class Example
    {
        private static void Main()
        {
            Execute().Wait();
        }

        static async Task Execute()
        {
            //var apiKey = Environment.GetEnvironmentVariable("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");
            var client = new SendGridClient("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");
            var from = new EmailAddress("dorianhaley1992@gmail.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("damonefreeman@gmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var HtmlContent = "and easy to do anywhere, even with C#";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}