using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace WebApplication1
{
    public class CustomMiddleWare
    {
        private ILogger<CustomMiddleWare> _logger;
        private readonly RequestDelegate _next;
        public CustomMiddleWare(ILogger<CustomMiddleWare> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _logger.LogError("BYEEE");
                _logger.LogInformation("HELLOOO");
                Stopwatch start = Stopwatch.StartNew();
                await _next(context);
                start.Stop();
                _logger.LogInformation(start.Elapsed.TotalSeconds.ToString());
            } catch (Exception exception)
            {
                  _logger.LogError(exception,
                    $"Request {context.Request?.Method}: {context.Request?.Path.Value} failed");
            }
        }
    }
}
