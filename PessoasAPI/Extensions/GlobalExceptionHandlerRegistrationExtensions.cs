using Microsoft.AspNetCore.Builder;
using PessoasAPI.Middleware;
using System;

namespace PessoasAPI.Extensions
{
    internal static class GlobalExceptionHandlerRegistrationExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app = app ?? throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
