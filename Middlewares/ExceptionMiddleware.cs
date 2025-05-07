using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CPRCMAPI.Middlewares
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;         
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try {
                await _next(context);
            }
            catch (Exception ex) {
               _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                //await context.Response.WriteAsync(ex.Source + "-->" + ex.Message);                
                await context.Response.WriteAsync("Error processing request.Server error.");
            }
                     
        }
    }
}
