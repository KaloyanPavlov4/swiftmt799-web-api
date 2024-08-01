using NLog;
using System.Net;

namespace swiftmt799_web_api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly NLog.ILogger _logger = LogManager.GetCurrentClassLogger();

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //Logs the error to a file located in logs/errors.log
            _logger.Error(exception, "An unhandled exception occurred");

            context.Response.ContentType = "application/json";
            if (exception.GetType() == typeof(ArgumentNullException) || exception.GetType() == typeof(ArgumentException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
