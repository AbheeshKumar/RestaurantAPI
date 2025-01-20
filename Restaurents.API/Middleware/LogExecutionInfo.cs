namespace Restaurents.API.Middleware;
using System.Diagnostics;
public class LogExecutionInfo(ILogger<LogExecutionInfo> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var watch = Stopwatch.StartNew();
        watch.Start();
        await next.Invoke(context);
        watch.Stop();

        if (watch.ElapsedMilliseconds > 400)
        {
            logger.LogWarning("Request [{verb}] at {Path} took {time} ms", 
                context.Request.Method,
                context.Request.Path,
                watch.ElapsedMilliseconds);
        }
    }
}
