namespace HRLeaveManagement.Application.Models.Mail
{
    public class EmailSettings
    {
        public string ApiKey { get; set; } = default!;
        public string FromAddress { get; set; } = default!;
        public string Sender { get; set; } = default!;
    }
}
