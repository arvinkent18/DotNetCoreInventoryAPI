using System.Net;
using Inventory.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Inventory.Web.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            UserNotFoundException ex => new NotFoundObjectResult(new { ex.Message }),
            ProductNotFoundException ex => new NotFoundObjectResult(new { ex.Message }),
            UserAlreadyExistsException ex => new ConflictObjectResult(new { ex.Message }),
            ProductAlreadyExistsException ex => new ConflictObjectResult(new { ex.Message }),
            InvalidOperationException ex => new BadRequestObjectResult(new { ex.Message }),
            _ => new ObjectResult(new { Message = "An unexpected error occurred." })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            }
        };

        context.ExceptionHandled = true;
    }
}