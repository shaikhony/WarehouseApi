using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;
using WarehouseApi.MedalWhere;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private  Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        if (ex is MedalException medalException)
        {
            context.Response.StatusCode = medalException.ErrorType switch
            {
                ErrorType.Critical => 500,
                ErrorType.Warning => 400,
                ErrorType.Information => 200,
                _ => 500, // يفترض أن يكون هذا السيناريو الافتراضي
            };

            return context.Response.WriteAsync($"{medalException.ErrorType}: {medalException.Message}");
        }
        else
        {
            context.Response.StatusCode = 500;
            return context.Response.WriteAsync($"Internal Server Error: {ex.Message}");
        }
    }
}
