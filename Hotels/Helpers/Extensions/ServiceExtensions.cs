using Hotels.Helpers.Jwt;
using Hotels.net.Helpers.Jwt;
using Hotels.Repositories;
using Hotels.Repositories.UserRepository;
using Hotels.Services.UserService;

namespace Hotels.Helpers.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAll(this IServiceCollection services)
        {
            services.AddScoped< UserRepository>();
            services.AddTransient<IJwtUtils, JwtUtils>();
            services.AddTransient< UserService>();
            

            return services;
        }

        
    }
}
