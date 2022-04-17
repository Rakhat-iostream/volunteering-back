using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Vol.V1.Emails
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly SendGridClient client;

        public SendGridEmailSender(SendGridClient client)
        {
            this.client = client;
        }

        public async Task SendAsync(string address, string subject, string body)
        {
            var message = MailHelper.CreateSingleEmail(
                new EmailAddress("iskrobraz@mail.ru", "Vol"),
                new EmailAddress(address),
                subject,
                null,
                body
            );

            await this.client.SendEmailAsync(message);
        }

        public async Task SendEmailAsync(MailMessage mail)
        {
            var message = MailHelper.CreateSingleEmailToMultipleRecipients(
                new EmailAddress(mail.From.Address, mail.From.DisplayName),
                mail.To.Select(x => new EmailAddress(x.Address, x.DisplayName)).ToList(),
                mail.Subject,
                mail.IsBodyHtml ? null : mail.Body,
                mail.IsBodyHtml ? mail.Body : null);

            await this.client.SendEmailAsync(message);
        }
    }
}
