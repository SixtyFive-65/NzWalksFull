using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace NZWalks.API.Middleware
{
    public class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> logger;
        private readonly RequestDelegate next;

        public ExceptionHandler(ILogger<ExceptionHandler> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                logger.LogError(ex,$"{errorId} : {ex.Message}");

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new 
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong! we are looking into this issue" 
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
