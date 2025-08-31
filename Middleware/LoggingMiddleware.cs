// LoggingMiddleware.cs
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var method = context.Request.Method;
        var path = context.Request.Path;

        _logger.LogInformation($"Incoming Request: {method} {path}");

        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var statusCode = context.Response.StatusCode;
        _logger.LogInformation($"Outgoing Response: {statusCode}");

        await responseBody.CopyToAsync(originalBodyStream);
    }
}
