
using FluentValidation;
using Inventory.Application.DTO;

namespace Inventory.Application.Validators;
public class GetAllProductsDtoValidator : AbstractValidator<GetAllProductsDto>
{
    public GetAllProductsDtoValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThan(0).WithMessage("PageIndex must be greater than 0.");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than 0.");
    }
}
