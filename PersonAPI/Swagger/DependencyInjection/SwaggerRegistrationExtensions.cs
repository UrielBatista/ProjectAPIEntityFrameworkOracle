using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace PessoasAPI.Swagger.DependencyInjection
{
    internal static class SwaggerRegistrationExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection serviceCollection)
        {
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

            return serviceCollection
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder applicationBuilder, IApiVersionDescriptionProvider provider)
        {
            _ = applicationBuilder ?? throw new ArgumentNullException(nameof(applicationBuilder));
            _ = provider ?? throw new ArgumentNullException(nameof(provider));

            return applicationBuilder
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
        }
    }
}
