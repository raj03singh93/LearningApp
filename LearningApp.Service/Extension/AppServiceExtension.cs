using AutoMapper;
using LearningApp.Service.Implementation;
using LearningApp.Service.Interface;
using LearningApp.Service.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace LearningApp.Service.Extension
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppService(this IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(config => config.AddProfile(new MappingProfile()));
            services.AddSingleton(mapperConfiguration.CreateMapper());

            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
