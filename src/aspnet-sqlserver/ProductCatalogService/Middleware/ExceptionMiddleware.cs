using ProductCatalogService.Repositories;
using System;

namespace ProductCatalogService.Middleware;

public class ExceptionMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionMiddleware> _logger;

  public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
  {
    _next = next;
    _logger = logger;
  }

  public async Task InvokeAsync(HttpContext httpContext)
  {
    try
    {
      await _next(httpContext);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An unhandled exception has occurred while executing the request.");
      await HandleExceptionAsync(httpContext, ex);
    }
  }

  private Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    context.Response.ContentType = "application/json";
    // context.Response.StatusCode = StatusCodes.Status500InternalServerError;

    var baseType = exception.GetType().BaseType;
    var isRepositoryException = baseType?.IsGenericType == true && baseType.GetGenericTypeDefinition() == typeof(RepositoryException<>);

    var response = new
    {
      context.Response.StatusCode,
      Message = isRepositoryException ? "A data access error occurred. Please try again later." : "Internal Server Error from the custom middleware.",
      Detailed = isRepositoryException ? null : new
      {
        exception.Message,
        exception.StackTrace
        // Add any other properties you want to include in the response
      }
    };

    return context.Response.WriteAsJsonAsync(response);
  }
}