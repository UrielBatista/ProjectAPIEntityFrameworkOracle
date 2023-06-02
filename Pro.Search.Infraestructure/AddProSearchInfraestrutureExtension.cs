using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pro.Search.Infraestructure.Repositories.Support.DependencyInjection;
using Pro.Search.Infraestructure.ServiceReferences;
using System.Diagnostics.CodeAnalysis;

namespace Pro.Search.Infraestructure
{
    [ExcludeFromCodeCoverage]
    public static class AddProSearchInfraestrutureExtension
    {
        public static IServiceCollection AddSearchInfraestruture(this IServiceCollection services,
            IConfiguration configuration)
        {
            _ = services.AddSearchRepository()
                .AddServiceReferences(configuration);
            return services;
        }
    }
}
