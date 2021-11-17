using Microsoft.Extensions.DependencyInjection;

namespace Pro.Search.Infraestructure.Repositories.Support.DependencyInjection
{
    public static class RepositoryRegistrationExtensions
    {
        public static IServiceCollection AddSearchRepository(this IServiceCollection services)
        {
            _ = services.AddTransient<IPessoasRepository, PessoasRepository>()
                .AddTransient<IFoodRepository, FoodRepository>();
                    
            return services;
        }
    }
}
