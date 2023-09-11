using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookStoreUser.Helpers;
using BookStoreUser.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BookStoreUser.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch(AppException ex)
            {
                int statusCode = ex.GetStatusCode();
                _logger.LogError(ex, ex.Message);
                httpContext.Response.StatusCode = statusCode;
                httpContext.Response.ContentType = "application/json";

                AppResponse appResponse = new AppResponse
                {
                    Status = "failed",
                    Message = ex.Message,
                };
                
                await httpContext.Response.WriteAsJsonAsync(appResponse);
            }
            catch (Exception ex)
            {
                string message = "An error occured, please try again.";
                _logger.LogError(ex, ex.Message);
                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                if (_env.IsDevelopment())
                {
                    AppResponse appResponse = new AppResponse
                    {
                        Status = "failed",
                         Message = ex.Message,
                    };
                    await httpContext.Response.WriteAsJsonAsync(appResponse);
                }
                else
                {
                    AppResponse appResponse = new AppResponse
                    {
                        Status = "failed",
                        Message = message,
                    };
                    await httpContext.Response.WriteAsJsonAsync(appResponse);
                }
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobalExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareClassTemplate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}

