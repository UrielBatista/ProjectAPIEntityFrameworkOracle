using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PessoasAPI.Context;
using PessoasAPI.Repository;
using Pro.Search.PersonDomains.PersonEngine.Commands;
using Pro.Search.PersonDomains.PersonEngine.Queries;
using System.Reflection;

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
            services.AddDbContext<ContextDB>(options => options.UseOracle(Configuration.GetConnectionString("OracleDBConnection")));
            services.AddTransient<IPessoasRepository, PessoasRepository>();
            services.AddMediatR(
                typeof(GetAllPersonQueryHandler).GetTypeInfo().Assembly, 
                typeof(PersonCreateCommandHandler).GetTypeInfo().Assembly,
                typeof(PersonUpdateCommandHandler).GetTypeInfo().Assembly,
                typeof(GetOnePersonQueryHandler).GetTypeInfo().Assembly);

            services.AddSwaggerGen();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwaggerUI();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
