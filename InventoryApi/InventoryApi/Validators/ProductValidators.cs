using FluentValidation;
using InventoryApi.DTOs;

namespace InventoryApi.Validators
{
    public class ProductValidators : AbstractValidator<ProductDto>
    {
        public ProductValidators() 
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(300).WithMessage("Product name cannot exceed 300 characters.");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(p => p.StockQuantity)
                .NotEmpty().WithMessage("Stock quantity is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be greater than 0.");
        }
    }
}
