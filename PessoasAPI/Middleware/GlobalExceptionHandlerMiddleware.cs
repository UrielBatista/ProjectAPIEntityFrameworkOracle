using Microsoft.AspNetCore.Http;
using Oracle.ManagedDataAccess.Client;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PessoasAPI.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            try
            {
                await this.next(context).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex, default).ConfigureAwait(false);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, CancellationToken cancellationToken = default)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            _ = exception ?? throw new ArgumentNullException(nameof(exception));

            var errorDetails = new ErrorDetails();
            var errors = new List<Dictionary<string, string>>();
            var errorDescription = new Dictionary<string, string>()
            {
                { exception.Source, exception.Message },
            };

            errors.Add(errorDescription);

            errorDetails.MessageTitle = exception is OracleException ? "Database error" : "Unknown error";
            errorDetails.StatusCode = (int)HttpStatusCode.InternalServerError;
            errorDetails.Errors = errors;

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(errorDetails.ToString(), cancellationToken).ConfigureAwait(false);
        }
    }
}
