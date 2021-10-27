using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Pro.Search.Infraestructure.Validators.DependencyInjection
{
    public static class ValidatorsRegistrationExtensions
    {
        public static IServiceCollection AddPersonEngineValidators(this IServiceCollection serviceCollection)
        {
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

            return serviceCollection.AddFluentValidation(fv =>
            {
                fv.ImplicitlyValidateChildProperties = true;
                _ = fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
        } 
    }
}
