using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace webapi.filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.LogError(context.Exception, "An unhandled exception occurred while processing the request.");

            // Create a generic error response
            var response = new
            {
                Message = "An error occurred while processing your request. Please try again later.",
                Detail = context.Exception.Message // You can customize the level of detail based on the environment
            };

            // Set the result to a generic error response with a 500 status code
            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            context.ExceptionHandled = true;
        }
    }
}
