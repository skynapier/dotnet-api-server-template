using System.Net;
using System.Text.Json;

public class ClientExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ClientExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ClientException ex)
        {
            await HandleClientExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleClientExceptionAsync(HttpContext context, ClientException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;

        var result = JsonSerializer.Serialize(new { error = exception.Message });
        return context.Response.WriteAsync(result);
    }
}
