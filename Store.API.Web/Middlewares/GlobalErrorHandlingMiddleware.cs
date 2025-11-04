using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Store.API.Domain.Exceptions.NotFoundExceptions;
using Store.API.Domain.Exceptions.UnAuthorizedException;
using Store.API.Domain.Exceptions.ValidationException;
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
                    await HandlingNotFoundEndPointAsync(context);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandlingErrorAsync(context, ex);
            }

        }

        private static async Task HandlingErrorAsync(HttpContext context, Exception ex)
        {

            //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                ErrorMessage = ex.Message
            };

            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadHttpRequestException => StatusCodes.Status400BadRequest,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                ValidationException => HandlingValidationEndPointAsync((ValidationException)ex,response),
                _ => StatusCodes.Status500InternalServerError
            };
            context.Response.StatusCode = response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandlingNotFoundEndPointAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                ErrorMessage = $"End Point {context.Request.Path} Is Not Found",
                StatusCode = StatusCodes.Status404NotFound
            };
            await context.Response.WriteAsJsonAsync(response);
        }
        private static int HandlingValidationEndPointAsync(ValidationException ex,ErrorDetails responce)
        {
            responce.Errors = ex.errors;
            return StatusCodes.Status400BadRequest;

        }
    }
}
