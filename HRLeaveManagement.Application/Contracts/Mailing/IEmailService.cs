using HRLeaveManagement.Application.Models.Mail;

namespace HRLeaveManagement.Application.Contracts.Mailing
{
    public interface IEmailService
    {
        Task<bool> SendMailAsync(EmailMessage email);
    }
}
