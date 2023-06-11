using FluentValidation;
using FluentValidation.AspNetCore;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PersonAPI.Extensions;
using PersonAPI.GraphQL;
using PessoasAPI.Extensions;
using PessoasAPI.Swagger.DependencyInjection;
using Pro.Search.Infraestructure;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.GraphQL.Schemas;
using Pro.Search.Infraestructure.Mappers;
using Pro.Search.Infraestructure.Validators;
using Pro.Search.PersonCommands.Queries;
using Pro.Search.PersonDomains.PersonEngine.Commons;
using Pro.Search.PersonDomains.PersonEngine.GraphQL.Types;
using System;
using System.Text;

namespace PessoasAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddControllers()
                
                // OData configuration controller
                .AddOData(option => 
                {
                    _ = option.Select();
                    _ = option.Filter();
                    _ = option.SetMaxTop(5);
                    _ = option.SkipToken();
                });
            // Database Connector
            //_ = services.AddDbContext<ISystemDBContext, SystemDBContext>(options =>
            //        options.UseOracle(Configuration.GetConnectionString("OracleDBConnection")));
            //_ = services.AddDbContext<ISystemDBContext, SystemDBContext>(options =>
            //        options.UseSqlite(Configuration.GetConnectionString("SqliteDBConnection")));
            _ = services.AddDbContext<ISystemDBContext, SystemDBContext>(options =>
                    options.UseMySQL(Configuration["CONNECT_STRING"])
                    .LogTo(Console.WriteLine, LogLevel.Information));

            _ = services.AddAutoMapper(typeof(PersonProfile).Assembly, typeof(FoodProfile).Assembly);

            //MassTransit Dependency injection
            //services.AddMassTransitExtension(Configuration);
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddConsole();
            });
            // Authorization dependency injection
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            _ = services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });

            // Versioning API dependency injection
            _ = services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(3, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            _ = services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            _ = services.AddCustomSwagger();
            
            _ = services.AddSearchInfraestruture(Configuration);
            
            _ = services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetOnePersonQuery).Assembly));

            // GraphQL dependency injection
            _ = services.AddScoped<PersonSchema>();
            _ = services.AddGraphQL(b => b
                .AddHttpMiddleware<PersonSchema>()
                .AddUserContextBuilder(httpContext => new GraphQLUserContext { User = httpContext.User })
                .AddSystemTextJson()
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
                .AddGraphTypes(typeof(PersonsTypes).Assembly));
            
            _ = services.AddValidatorsFromAssemblyContaining<GraphQLPersonsInfoDtoValidator>();
            _ = services.AddFluentValidationAutoValidation();
            _ = services.AddFluentValidationClientsideAdapters();

            services.AddGraphQLExtensions();

            _ = services.AddSwaggerGen();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
            }

            _ = app.UseGlobalExceptionHandler();
            _ = app.UseRouting();

            _ = app.UseWebSockets();

            _ = app.UseAuthentication();
            _ = app.UseAuthorization();

            _ = app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
                endpoints.MapGraphQL("/v2/graphql").WithOptions(new GraphQLServerOptions
                {
                    EnableBatching = true
                });
            });

            _ = app.UseGraphQL<PersonSchema>();
            _ = app.UseGraphQLPlayground();

            _ = app.UseCustomSwagger(provider);
        }
    }
}
