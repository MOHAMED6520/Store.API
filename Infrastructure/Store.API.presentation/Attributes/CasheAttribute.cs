using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Store.API.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.presentation.Attributes
{
    public class CasheAttribute(int timeinsec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var CasheService =  context.HttpContext.RequestServices.GetRequiredService<IServiceManger>().CasheService;
            var cashKey = GenerateKey(context.HttpContext.Request);
            var result =await CasheService.GetCasheValueAsync(cashKey);


            if(!string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "application/json",
                    Content = result
                };
                return;
            }
             var contextResult = await next.Invoke();
            if(contextResult.Result is OkObjectResult okObjectResult )
            {
              await  CasheService.SetCasheValueAsync(cashKey, okObjectResult, TimeSpan.FromDays(timeinsec));

            }
        }
        private string GenerateKey(HttpRequest request)
        {
            var builder = new StringBuilder();
            builder.Append(request.Path);
            foreach(var item in request.Query.OrderBy(p=>p.Key))
            {
                builder.Append($"|{item.Key}-{item.Value}");
            }
            return builder.ToString();
        }
    }
}
