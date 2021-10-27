using Microsoft.Extensions.DependencyInjection;
using Pro.Search.PersonDomains.PersonEngine;

namespace Pro.Search.Infraestructure.Repositories.Support.DependencyInjection
{
    public static class RepositoryRegistrationExtensions
    {
        public static IServiceCollection AddSearchRepository(this IServiceCollection services)
        {
            _ = services.AddTransient<IPessoasRepository, PessoasRepository>();
            return services;
        }
    }
}
