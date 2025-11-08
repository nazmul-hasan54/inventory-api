using FluentValidation;
using InventoryApi.DTOs;

namespace InventoryApi.Validators
{
    public class OrderItemValidators: AbstractValidator<OrderItemDto>
    {
        public OrderItemValidators() 
        {
            RuleFor(oi => oi.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(oi => oi.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(oi => oi.UnitPrice)
                .NotEmpty().WithMessage("Unit price is required.")
                .GreaterThan(0).WithMessage("Unit price must be greater than 0.");
        }
    }
}
