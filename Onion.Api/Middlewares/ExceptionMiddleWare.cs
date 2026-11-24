using Onion.Api.Errors;
using System.Net;

namespace Onion.Api.Middlewares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _environment;
        public ExceptionMiddleWare(RequestDelegate Next, ILogger<ExceptionMiddleWare> Logger, IHostEnvironment Environment)
        {
            _next = Next;
            _logger = Logger;
            _environment = Environment;
        }

        // invoke Async
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application.json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var Response = _environment.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                :   new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
