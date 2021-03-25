using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Service.ViewModel.Profiles.Candidate;
using Service.ViewModel.Profiles.Employer;
using Service.ViewModel.Profiles.Skills;

namespace Presentation.Configration.Configrations
{
    public static class ScopedAutoMapperConfiguration
    {
        public static IServiceCollection AddScopedAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mapper =>
            {
                mapper.AddProfile(new CandidateProfile());
                mapper.AddProfile(new EmployerProfile());
                mapper.AddProfile(new SkillProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
