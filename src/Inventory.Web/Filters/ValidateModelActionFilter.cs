using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Inventory.Web.Filters;

public class ValidateModelActionFilter : IActionFilter
{
    private readonly IServiceProvider _serviceProvider;

    public ValidateModelActionFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
   
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var model = context.ActionArguments.Values.FirstOrDefault();

        if (model == null)
        {
            return;
        }

        var modelType = model.GetType();
        var validatorType = typeof(IValidator<>).MakeGenericType(modelType);
        var validator = _serviceProvider.GetService(validatorType) as IValidator;

        if (validator == null)
        {
            return;
        }

        var validationContext = new ValidationContext<object>(model);
        var validationResult = validator.Validate(validationContext);

        if (!validationResult.IsValid)
        {
            context.Result = new BadRequestObjectResult(new ValidationProblemDetails
            {
                Errors = validationResult.Errors.ToDictionary(
                    e => e.PropertyName,
                    e => new[] { e.ErrorMessage }
                )
            });
        }
    }
}