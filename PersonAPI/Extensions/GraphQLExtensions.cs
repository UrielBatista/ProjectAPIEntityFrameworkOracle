using Microsoft.Extensions.DependencyInjection;
using Pro.Search.Infraestructure.GraphQL.Mutations;
using Pro.Search.Infraestructure.GraphQL.Queries;
using Pro.Search.Infraestructure.GraphQL.Subscriptions;

namespace PersonAPI.Extensions
{
    public static class GraphQLExtensions
    {
        public static void AddGraphQLExtensions(this IServiceCollection services)
        {
            _ = services.AddGraphQLServer()
                .AddQueryType<PersonQueryHotChocolate>()
                .AddMutationType<PersonMutation>()
                .AddSubscriptionType<PersonSubscriptions>()
                .AddInMemorySubscriptions()
                .AddMutationConventions()
                .AddDefaultTransactionScopeHandler()
                .AddExportDirectiveType()
                .AddProjections()
                .AddFiltering()
                .AddSorting();
        }
    }
}
