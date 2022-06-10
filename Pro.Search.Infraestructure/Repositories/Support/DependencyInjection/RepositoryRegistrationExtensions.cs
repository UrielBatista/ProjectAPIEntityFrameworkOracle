using Microsoft.Extensions.DependencyInjection;

namespace Pro.Search.Infraestructure.Repositories.Support.DependencyInjection
{
    public static class RepositoryRegistrationExtensions
    {
        public static IServiceCollection AddSearchRepository(this IServiceCollection services)
        {
            _ = services.AddTransient<IPersonsRepository, PersonsRepository>()
                .AddTransient<IFoodRepository, FoodRepository>()
                .AddTransient<IUserRepository, UserRepository>();
                    
            return services;
        }
    }
}
