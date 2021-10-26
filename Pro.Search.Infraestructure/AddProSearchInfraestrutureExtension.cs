using Microsoft.Extensions.DependencyInjection;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.Infraestructure.Repositories.Support;
using System.Diagnostics.CodeAnalysis;

namespace Pro.Search.Infraestructure
{
    [ExcludeFromCodeCoverage]
    public static class AddProSearchInfraestrutureExtension
    {
        public static IServiceCollection AddSearchInfraestruture(this IServiceCollection services)
        {
            _ = services.AddTransient<IPessoasRepository, PessoasRepository>();
            return services;
        }
    }
}
