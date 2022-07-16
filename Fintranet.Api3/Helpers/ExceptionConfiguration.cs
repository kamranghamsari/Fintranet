using Fintranet.Entities.Helpers;
using System.Text.Json;

namespace Fintranet.Api3.Helpers;

/// <summary>
/// Exception configuration
/// </summary>
public static class ExceptionConfiguration
{
    /// <summary>
    /// Add exception configuration
    /// </summary>
    /// <param name="app"></param>
    public static void AddExceptionConfiguration(IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}


/// <summary>
/// Exception middleware
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    /// <summary>
    /// Invoke
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception).ConfigureAwait(false);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return context.Response.WriteAsync(text: JsonSerializer.Serialize(new BaseResponseDto()
        {
            responseCode = ResponseCode.Fail,
            responseInformation = "Internal server error."
        }));
    }
}