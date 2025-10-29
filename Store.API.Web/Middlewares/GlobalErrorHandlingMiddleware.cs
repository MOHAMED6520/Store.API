using Microsoft.AspNetCore.Http;
using Store.API.Domain.Exceptions.NotFoundExceptions;
using Store.API.Shared.ErrorsModels;

namespace Store.API.Web.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next , ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next= next ;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try 
            {
                await _next.Invoke(context);

                if(context.Response.StatusCode==StatusCodes.Status404NotFound)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails()
                    {
                        ErrorMessage = $"End Point {context.Request.Path} Is Not Found",
                        StatusCode= StatusCodes.Status404NotFound
                    };
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ErrorDetails()
                {
                    ErrorMessage = ex.Message
                };

                response.StatusCode = ex switch
                {
                    ProductNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                context.Response.StatusCode = response.StatusCode;
                await context.Response.WriteAsJsonAsync(response);
            }
            
        }
    }
}
