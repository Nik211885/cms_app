
using backend.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace backend.Middleware
{
    public class ExceptionMiddlewareHandling : IMiddleware
    {
        private readonly ILogger<ExceptionMiddlewareHandling> _logger;
        public ExceptionMiddlewareHandling(ILogger<ExceptionMiddlewareHandling> logger)
        {
            _logger = logger;
        }
        public async Task HandlingExceptionAsync(Exception ex, HttpContext context)
        {
            OkObjectResult response = ex.GetType().GetInterface(nameof(IException)) == typeof(IException) 
                ? ResponseMessage.Warning(ex.Message) : ResponseMessage.Error(ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 200;
            var byteResponse = UTF8Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response.Value));
            await context.Response.Body.WriteAsync(byteResponse);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogInformation("Start running in pipeline exception");
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors {ex.Message}");
                await HandlingExceptionAsync(ex, context);
            }
        }
    }
}
