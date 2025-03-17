using HRLeaveManagement.Application.Contracts.Mailing;
using HRLeaveManagement.Application.CustomLogging;
using HRLeaveManagement.Application.Models.Mail;
using HRLeaveManagement.Infrastructure.CustomLogging;
using HRLeaveManagement.Infrastructure.MailingService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrasstructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
