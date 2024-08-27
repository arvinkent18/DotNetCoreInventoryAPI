using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Inventory.Web.Filters
{
    public class ResponseFormatFilter : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var originalValue = objectResult.Value;
                bool isSuccess = objectResult.StatusCode >= 200 && objectResult.StatusCode < 300;

                if (context.Result is BadRequestObjectResult badRequestResult &&
                    badRequestResult.Value is ValidationProblemDetails validationProblem)
                {
                    objectResult.Value = new
                    {
                        success = false,
                        message = "One or more validation errors occurred.",
                        data = (object)null,
                        errors = validationProblem.Errors
                    };
                }
                else
                {
                    objectResult.Value = new
                    {
                        success = isSuccess,
                        message = isSuccess ? "Request successful" : "An error occurred",
                        data = isSuccess ? originalValue : (object)null,
                        errors = isSuccess ? null : GetErrorDetails(objectResult)
                    };
                }
            }
            base.OnResultExecuting(context);
        }

        private object GetErrorDetails(ObjectResult objectResult)
        {
            if (objectResult.Value is ValidationProblemDetails validationProblem)
            {
                return validationProblem.Errors;
            }

            return new
            {
                error = objectResult.Value?.ToString() ?? "Unknown error"
            };
        }
    }
}
