using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Service.Interface.IService;
using Service.ViewModel.VM;

namespace CandidateManagment.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TokenManagerMiddleware
    {
        private readonly RequestDelegate next;

        public TokenManagerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, ITokenService service)
        {
            var token = "";
            string requestPath = httpContext.Request.Path.Value;

            if (requestPath.Contains("Candidates") || requestPath.Contains("Employers") || requestPath.Contains("Skills"))
            {
                try
                {
                    await next.Invoke(httpContext);
                }
                catch (Exception e)
                {
                    await HandleErrorAsync(httpContext, e);
                    return;
                }

            }
            else
            {

                try
                {
                    token = httpContext.Request.Headers["authorization"].Single().Split(" ").Last().Trim();

                }
                catch (Exception exception)
                {
                    await HandleErrorAsync(httpContext, exception);
                    return;
                }

                try
                {
                    if (service.CheckToken(token))
                    {


                        await next(httpContext);

                        return;
                    }
                }
                catch (Exception ex)
                {
                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    var lists = new List<string>();
                    lists.Add(ex.Message);
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new FailureResponse
                    {
                        Code = (int)HttpStatusCode.Forbidden,
                        Error = lists
                    }));
                    return;
                }

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                var list = new List<string>();
                list.Add("401 Unauthorized");
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new FailureResponse
                {
                    Code = (int)HttpStatusCode.Unauthorized,
                    Error = list
                }));

                return;
            }
        }

        public static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var response = new { message = exception.Message };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 401;

            var list = new List<string>();
            list.Add("401 Unauthorized");

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                code = (int)HttpStatusCode.Unauthorized,
                error = list
            }));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TokenManagerExtensions
    {
        public static IApplicationBuilder UseTokenManagerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenManagerMiddleware>();
        }
    }
}
