using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace D4PrototypeLearningPlatform.Middleware;

//https://stackoverflow.com/questions/58444525/how-to-log-the-selected-asp-net-core-mvc-route
public class UrlLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UrlLoggerMiddleware> _logger;

    public UrlLoggerMiddleware(RequestDelegate requestDelegate, ILogger<UrlLoggerMiddleware> logger)
    {
        _next = requestDelegate;
        _logger = logger;
    }

    public Task Invoke(HttpContext httpContext)
    {
        _logger.LogInformation(httpContext.Request.GetDisplayUrl());
        var nextTask = _next.Invoke(httpContext);
        return nextTask;
    }
}