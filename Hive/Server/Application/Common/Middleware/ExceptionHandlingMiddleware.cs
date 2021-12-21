using Hive.Domain.Errors;
using Hive.Server.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using HiveApplicationException = Hive.Domain.Errors.ApplicationException;

namespace Hive.Server.Application.Common.Middleware
{
    /// <summary>
    /// Middleware to handle exceptions on Request Pipeline
    /// </summary>
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            int statusCode = GetStatusCode(e);

            RequestValidationError response = new(GetTitle(e), statusCode, e.Message, GetErrors(e));

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            // Attach Error data to response
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));

        }

        private static string[] GetErrors(Exception e)
        {
            string[] errors = null;
            if (e is ValidationException validationException)
            {
                errors = validationException.Failures;
            }

            return errors;
        }

        private static string GetTitle(Exception e) => e switch
        {
            HiveApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };

        private static int GetStatusCode(Exception e) => e switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
