using Clean.Application.AutoMapper;

namespace Clean.WebApi.Configurations
{
    public static class AutoMapperConfig
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(Clean.Application.AutoMapper.AutoMapperConfiguration));// if AddAutoMapper comes from automapper extension DI package
            AutoMapperConfiguration.RegisterMappings();


        }
    }
}
