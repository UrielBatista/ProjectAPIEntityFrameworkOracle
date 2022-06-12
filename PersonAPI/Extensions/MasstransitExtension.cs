using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PessoasAPI.Extensions;

public static class MasstransitExtension
{
    public static void AddMassTransitExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration.GetConnectionString("RabbitMq"));
            });
        });
        services.AddMassTransitHostedService();
    }
}