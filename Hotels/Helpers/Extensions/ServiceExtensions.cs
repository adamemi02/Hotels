using Hotels.Repositories;
using Hotels.Repositories.UserRepository;
using Hotels.Services.UserService;


namespace Hotels.Helpers.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
           
            services.AddTransient<IUserRepository, UserRepository>();
   

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            
            services.AddTransient< UserService>();
           
            return services;
        }    
        
       
    }
}
