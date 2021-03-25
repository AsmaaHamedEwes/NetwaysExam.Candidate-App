using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CandidateManagment.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(httpContext, exception);
            }
        }

        public static Task HandleErrorAsync(HttpContext context, Exception exception)
        {



            var response = new { message = exception.Message };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var errorList = new List<string>();
            if (exception.InnerException != null)
            {
                errorList.Add(exception.InnerException.Message);
                var num = ((SqlException)exception.InnerException).Number;
                errorList.Add(num + "");
            }
            else
                errorList.Add(exception.Message);


            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                code = 500,
                error = errorList
            }));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
