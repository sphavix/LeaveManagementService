using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationServiceCollectionExtensions).Assembly);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceCollectionExtensions).Assembly));

            services.AddValidatorsFromAssembly(typeof(ApplicationServiceCollectionExtensions).Assembly);

            return services;

        }
    }
}
