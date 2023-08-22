using Microsoft.Extensions.DependencyInjection;
using Pro.Search.Infraestructure.Services;

namespace Pro.Search.Infraestructure.Repositories.Support.DependencyInjection
{
    public static class RepositoryRegistrationExtensions
    {
        public static IServiceCollection AddSearchRepository(this IServiceCollection services)
        {
            _ = services.AddTransient<IPersonsRepository, PersonsRepository>()
                .AddTransient<IFoodRepository, FoodRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<ITopicCreatePersonSubscriptionService, TopicCreatePersonSubscriptionService>();
                    
            return services;
        }
    }
}
