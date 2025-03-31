using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyProject.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace MyProject.Infrastructure.Middleware;

public class ErrorHandlingMiddleware(
    RequestDelegate next,
    ILogger<ErrorHandlingMiddleware> logger,
    IHostingEnvironment environment)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, context.Response);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpResponse response)
    {
        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            case ApiException apiEx:
                logger.LogWarning(apiEx, "API error");
                problemDetails.Status = apiEx.StatusCode;
                problemDetails.Title = apiEx.Message;
                problemDetails.Detail = environment.IsDevelopment() ? apiEx.ToString() : "A server error occurred."; 
                break;
            case UnauthorizedAccessException unauthorizedEx:
                logger.LogWarning(unauthorizedEx, "Unauthorized Access");
                problemDetails.Status = StatusCodes.Status401Unauthorized;
                problemDetails.Title = "Unauthorized";
                problemDetails.Detail = "You are not authorized to access this resource.";
                break;
            case KeyNotFoundException notFoundEx:
                logger.LogWarning(notFoundEx, "Resource Not Found");
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Resource Not Found";
                problemDetails.Detail = notFoundEx.Message;
                break;
            case ValidationException valEx:
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Validation error";
                problemDetails.Extensions["errors"] = valEx.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
                break;
            default:
                logger.LogError(exception, "Unhandled exception");
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Internal Server Error";
                problemDetails.Detail = environment.IsDevelopment() ? exception.ToString() : "An unexpected error occurred. Please try again later.";
                break;
        }

        problemDetails.Instance = context.Request.Path; // Optional: Add the request path

        response.ContentType = "application/json";
        response.StatusCode = problemDetails.Status ?? 500;  // Ensure StatusCode is not null

        var json = JsonSerializer.Serialize(problemDetails);
        await response.WriteAsync(json);
    }
}