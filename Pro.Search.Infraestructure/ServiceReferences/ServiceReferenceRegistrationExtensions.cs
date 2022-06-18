using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pro.Search.Infraestructure.ServiceReferences.CepApi.DependencyInjection;
using Pro.Search.Infraestructure.ServiceReferences.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.ServiceReferences
{
    [ExcludeFromCodeCoverage]
    internal static class ServiceReferenceRegistrationExtensions
    {
        public static IServiceCollection AddServiceReferences(
            this IServiceCollection serviceCollection, 
            IConfiguration configuration)
        {
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

            return serviceCollection
                .AddCepApi((provider, httpClient) =>
                {
                    var settings = provider.GetRequiredService<IOptions<ServiceReferencesSettings>>().Value;
                    _ = httpClient.BaseAddress = settings.CepApiUrl;
                });
        }
    }
}
