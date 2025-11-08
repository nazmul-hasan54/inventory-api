using FluentValidation;
using InventoryApi.DTOs;

namespace InventoryApi.Validators
{
    public class OrderValidators : AbstractValidator<OrderDto>
    {
        public OrderValidators() 
        {
            RuleFor(o => o.CustomerName)
                .MaximumLength(300).WithMessage("Customer name cannot exceed 300 characters.");

            RuleFor(o => o.TotalAmount)
                .NotEmpty().WithMessage("Total amount is required.")
                .GreaterThan(0).WithMessage("Total amount must be greater than or equal to 0.");
        }
    }
}
