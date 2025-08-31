// AuthenticationMiddleware.cs
public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthenticationMiddleware> _logger;
    private const string ValidToken = "test-token"; // Replace with your test token

    public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            _logger.LogWarning("Authorization header missing or malformed.");
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { error = "Unauthorized: Missing or invalid token format." });
            return;
        }

        var token = authHeader.Substring("Bearer ".Length).Trim();

        if (token != ValidToken)
        {
            _logger.LogWarning("Invalid token.");
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { error = "Unauthorized: Invalid token." });
            return;
        }

        await _next(context);
    }
}
