using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PessoasAPI.Extensions;
using PessoasAPI.Swagger.DependencyInjection;
using Pro.Search.Infraestructure;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Mappers;
using Pro.Search.PersonCommands.Queries;

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
            services.AddControllers();
            services.AddDbContext<IContextDB, ContextDB>(options => options.UseOracle(Configuration.GetConnectionString("OracleDBConnection")));
            services.AddAutoMapper(typeof(PersonProfile).Assembly, typeof(FoodProfile).Assembly);

            _ = services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(2, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            _ = services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            _ = services.AddCustomSwagger();
            
            services.AddSearchInfraestruture();
            services.AddMediatR(
                typeof(GetOnePersonQuery).Assembly);

            services.AddSwaggerGen();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            _ = app.UseCustomSwagger(provider);

            _ = app.UseGlobalExceptionHandler();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
