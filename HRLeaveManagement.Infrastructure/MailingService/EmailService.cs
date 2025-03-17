using HRLeaveManagement.Application.Contracts.Mailing;
using HRLeaveManagement.Application.Models.Mail;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace HRLeaveManagement.Infrastructure.MailingService
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettigns { get; }
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettigns = emailSettings.Value;
        }

        public async Task<bool> SendMailAsync(EmailMessage email)
        {
            var sendGridClient = new SendGridClient(_emailSettigns.ApiKey); // instantiate the instance of the SendGrid wuth ApiKey

            var emailTo = new EmailAddress(email.To);
            var emailFrom = new EmailAddress
            {
                Email = _emailSettigns.FromAddress,
                Name = _emailSettigns.Sender
            };

            var messageBody = MailHelper.CreateSingleEmail(emailFrom, emailTo, email.Subject, email.Body, email.Body);

            var response = await sendGridClient.SendEmailAsync(messageBody);

            return response.IsSuccessStatusCode;
        }
    }
}
