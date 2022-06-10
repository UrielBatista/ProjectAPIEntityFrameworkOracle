using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace PessoasAPI.Swagger
{
    public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
            => this.apiVersionDescriptionProvider = apiVersionDescriptionProvider;

        public void Configure(SwaggerGenOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));

            var assembly = this.GetType().Assembly;
            var assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
            var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

            foreach(var apiVersionDescription in this.apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                var openApiInfo = new OpenApiInfo
                {
                    Description = assemblyDescription,
                    Title = assemblyProduct,
                    Version = apiVersionDescription.ApiVersion.ToString(),
                };

                if (apiVersionDescription.IsDeprecated)
                {
                    openApiInfo.Description += " - A Versao dessa API esta depreciada";
                }

                options.SwaggerDoc(apiVersionDescription.GroupName, openApiInfo);
            }

            var securityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Name = "JWT Authentication",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme,
                },
                Scheme = "bearer",
                Type = SecuritySchemeType.Http,
            };
            options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement { { securityScheme, Array.Empty<string>() } });

            var filePath = Path.Combine(AppContext.BaseDirectory, "PersonAPI.xml");
            options.IncludeXmlComments(filePath);
        }
    }
}
