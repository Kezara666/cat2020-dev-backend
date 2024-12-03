using System.Net;
using System.Text.Json;
using CAT20.WebApi.ExceptionHandling.Models;
using Serilog;

namespace CAT20.WebApi.CustomMiddlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next,ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                Log.Error(ex, "An unhandled exception occurred. Details:");

            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            ResponseModel exModel = new ResponseModel();

            switch (exception)
            {
                case ApplicationException ex:
                    exModel.responseCode = (int)HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    exModel.responseMessage = "Application Exception Occured, please retry after sometime.";
                    break;
                case FileNotFoundException ex:
                    exModel.responseCode = (int)HttpStatusCode.NotFound;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    exModel.responseMessage = "The requested resource is not found.";
                    break;
                case OutOfMemoryException ex:
                    exModel.responseCode = (int)HttpStatusCode.ServiceUnavailable;
                    response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                    exModel.responseMessage = "Out of Memory Exception, please retry after sometime.";
                    break;
                default:
                    exModel.responseCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exModel.responseMessage = "Internal Server Error, Please retry after sometime";
                    break;

            }
            // _logger.LogError(exception, "An unhandled exception occurred. Details:");
            var exResult = JsonSerializer.Serialize(exModel);
            await context.Response.WriteAsync(exResult);
        }





        
    }
}
