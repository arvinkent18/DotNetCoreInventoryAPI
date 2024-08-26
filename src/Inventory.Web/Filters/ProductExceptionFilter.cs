using Inventory.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Filters;
public class ProductExceptionFilter : IAsyncExceptionFilter
{
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;

        IActionResult result;
        int statusCode;
        string message;

        if (exception is ProductNotFoundException)
        {
            statusCode = StatusCodes.Status404NotFound;
            message = exception.Message;
        }
        else
        {
            statusCode = StatusCodes.Status500InternalServerError;
            message = "An unexpected error occurred.";
        }

        result = new ObjectResult(new
        {
            success = false,
            message
        })
        {
            StatusCode = statusCode
        };

        context.Result = result;

        await Task.CompletedTask;
    }
}

