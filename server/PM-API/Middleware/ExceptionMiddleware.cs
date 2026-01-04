using System.Net;
using Newtonsoft.Json;
using PM_API.DTOs;
using PM_API.Exceptions;

namespace PM_API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var message = string.Empty;
                var exceptionType = ex.GetType();
                if (exceptionType == typeof(ArgumentNullException))
                {
                    message = "Null Argument Exception " + ex.Message;
                }
                else
                {
                    message = ex.Message.ToString();
                }


                _logger.LogError(ex, message);
                if(exceptionType.BaseType == typeof(ProjectManagementException))
                    await HandleProjectManagementExceptionAsync(httpContext, ex);
                else
                    await HandleServerExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleProjectManagementExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception.GetType() == typeof(NotFoundException))
                return HandleExceptionAsync(context, exception.Message, (int)HttpStatusCode.NotFound, notFound: true);
            
            var statusCode = (int)HttpStatusCode.BadRequest;
            return HandleExceptionAsync(context, exception.Message, statusCode);
        }

        private Task HandleServerExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError($"[Exception Middleware] Caught exception with message: {exception.Message}");
            var statusCode = (int)HttpStatusCode.InternalServerError;
            return HandleExceptionAsync(context, "Something went wrong...", statusCode);
        }

        private static Task HandleExceptionAsync(HttpContext context, string message, int statusCode, bool notFound = false)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            var error = new HttpErrorDto(message, isNotFound: notFound);
            var jsonString = JsonConvert.SerializeObject(error);
            return context.Response.WriteAsync(jsonString);
        }
}