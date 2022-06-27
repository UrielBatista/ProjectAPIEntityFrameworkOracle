using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.ServiceReferences.CepApi.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class CepApiRegistrationExtensions
    {
        public static IServiceCollection AddCepApi(
            this IServiceCollection serviceCollection, 
            Action<IServiceProvider, HttpClient> configureClient)
        {
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
            _ = configureClient ?? throw new ArgumentNullException(nameof(configureClient));

            return serviceCollection
                .AddHttpClient<ICepApiResources, CepApiResources>("CepApi", configureClient).Services;
        }
    }
}
