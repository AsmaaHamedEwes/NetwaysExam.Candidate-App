using Microsoft.Extensions.DependencyInjection;
using Service.Implementation.Services;
using Service.Interface.IService;

namespace Presentation.Config.ConfigurationService
{
    public static class ScopedServiceConfiguration
    {
        public static IServiceCollection AddScopedService(this IServiceCollection services)
        {
            services.AddTransient(typeof(ICandidateService), typeof(CandidateService));
            services.AddTransient(typeof(IEmployerService), typeof(EmployerService));
            services.AddTransient(typeof(ISkillService), typeof(SkillsService));
            services.AddTransient(typeof(ITokenService), typeof(TokenService));
            return services;
        }
    }
}
