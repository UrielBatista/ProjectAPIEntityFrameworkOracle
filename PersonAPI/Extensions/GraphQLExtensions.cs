﻿using AppAny.HotChocolate.FluentValidation;
using FluentValidation.AspNetCore;
using HotChocolate;
using HotChocolate.AspNetCore.Serialization;
using Microsoft.Extensions.DependencyInjection;
using PersonAPI.Middleware;
using Pro.Search.Infraestructure.GraphQL.Mutations;
using Pro.Search.Infraestructure.GraphQL.Queries;
using Pro.Search.Infraestructure.GraphQL.Subscriptions;

namespace PersonAPI.Extensions
{
    public static class GraphQLExtensions
    {
        public static void AddGraphQLExtensions(this IServiceCollection services)
        {
            _ = services.AddHttpResponseFormatter(_ => new GlobalExceptionCustomHttpResponseFormatter());

            _ = services.AddGraphQLServer()
                .AddQueryType<PersonQueryHotChocolate>()
                .AddMutationType<Mutation>()
                .AddTypeExtension<PersonMutation>()
                .AddTypeExtension<PersonWithFoodsMutation>()
                .AddSubscriptionType<PersonSubscriptions>()
                .AddInMemorySubscriptions()
                .AddDefaultTransactionScopeHandler()
                .AddExportDirectiveType()
                .AddProjections()
                .AddFiltering()
                .AddFluentValidation(x => x.UseDefaultErrorMapper())
                .AddSorting();
        }
    }
}
