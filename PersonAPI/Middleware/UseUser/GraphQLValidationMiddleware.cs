using FluentValidation;
using HotChocolate;
using HotChocolate.Resolvers;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PersonAPI.Middleware
{
    public class GraphQLValidationMiddleware<T>
    {
        public const string USER_CONTEXT_DATA_KEY = "Person";

        private readonly FieldDelegate _next;

        public GraphQLValidationMiddleware(FieldDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            if (context.ArgumentValue<T>("input") is T argument )
            {
                var validator = context.Services.GetRequiredService<IValidator<T>>();
                var validationResult = await validator.ValidateAsync(argument, context.RequestAborted);

                if (!validationResult.IsValid)
                {
                    var errorDictionary = validationResult.Errors.ToDictionary(
                    validationError => validationError.PropertyName,
                    validationError => new[] { validationError.ErrorMessage });

                    var jsonErrorDictionary = JsonSerializer.Serialize(errorDictionary);

                    throw new GraphQLException(ErrorBuilder.New()
                        .SetMessage("Validation failed")
                        .SetExtension("validationErrors", jsonErrorDictionary)
                        .Build());
                }
            }

            await _next(context);
        }
    }
}
