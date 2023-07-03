using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BookStoreManager.Middleware
{
    public class TimeMeasureMiddleware : IMiddleware
    {
        private readonly Stopwatch _stopwatch;
        private readonly ILogger<TimeMeasureMiddleware> _logger;
        public TimeMeasureMiddleware(ILogger<TimeMeasureMiddleware> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();

            var elapsedMilisec = _stopwatch.ElapsedMilliseconds / 1000;
            if (elapsedMilisec > 4)
            {
                _logger.LogInformation($"Mathod: [{context.Request.Method}] at [{context.Request.Path}] took {elapsedMilisec} seconds");
            }
        }
    }
}
