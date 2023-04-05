using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace D4PrototypeLearningPlatform.Middleware;

//https://stackoverflow.com/questions/58444525/how-to-log-the-selected-asp-net-core-mvc-route
public class PerformanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceMiddleware> _logger;

    public PerformanceMiddleware(RequestDelegate requestDelegate, ILogger<PerformanceMiddleware> logger)
    {
        _next = requestDelegate;
        _logger = logger;
    }

    public Task Invoke(HttpContext httpContext)
    {
        //var watch = new Stopwatch();
        //watch.Start();
        _logger.LogInformation(httpContext.Request.GetDisplayUrl());
        var nextTask = _next.Invoke(httpContext);
        //nextTask.ContinueWith(t =>
        //{
        //    var time = watch.ElapsedMilliseconds;
        //    var requestString = $"[{httpRequest.Method}]{httpRequest.Path}?{httpRequest.QueryString}";
        //    if (t.Status == TaskStatus.RanToCompletion)
        //    {
        //        ..log.Info..($"{time}ms {requestString}");
        //    }
        //    else
        //    {
        //        ..log.Warn..($"{time}ms [{t.Status}] - {requestString}", t.Exception?.InnerException);
        //    }
        //});
        return nextTask;
    }
}