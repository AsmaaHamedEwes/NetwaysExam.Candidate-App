using Infrastructure.IRepository;
using Infrastructure.IRepository.IUserRepository;
using Infrastructure.Repository;
using Infrastructure.Repository.UserRepository;
using Microsoft.Extensions.DependencyInjection;


namespace Presentation.Config.ConfigurationService
{
    public static class ScopedRepositoryConfiguration
    {
        public static IServiceCollection AddScopedRepository(this IServiceCollection services)
        {
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient(typeof(IUserRepository<>), typeof(UserRepository<>));
            return services;
        }
    }
}
