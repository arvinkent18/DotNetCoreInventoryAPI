using Inventory.Application.Exceptions;

namespace Inventory.Web.Middlewares;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ProductNotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/json";
            var response = new
            {
                success = false,
                message = ex.Message
            };
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var response = new
            {
                success = false,
                message = "An unexpected error occurred."
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
