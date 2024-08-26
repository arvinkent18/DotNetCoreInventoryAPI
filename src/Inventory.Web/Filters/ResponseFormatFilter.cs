using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Filters;
    public class ResponseFormatFilter : ResultFilterAttribute
    {
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            var originalValue = objectResult.Value;
            bool isSuccess = objectResult.StatusCode >= 200 && objectResult.StatusCode < 300;

            objectResult.Value = new
            {
                success = isSuccess,
                message = isSuccess ? "Request successful" : "An error occurred",
                data = isSuccess ? originalValue : null,
                errors = isSuccess ? null : GetErrorDetails(objectResult)
            };
        }
        base.OnResultExecuting(context);
    }

    private object GetErrorDetails(ObjectResult objectResult)
    {
        return new
        {
            error = objectResult.Value?.ToString() ?? "Unknown error"
        };
    }
}

