using FluentValidation;
using InventoryApi.DTOs;

namespace InventoryApi.Validators
{
    public class UserValidators: AbstractValidator<UserDto>
    {
        public UserValidators() 
        {
            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Username is required!")
                .MaximumLength(300).WithMessage("Username cannot exceed 300 characters.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required!")
                .MinimumLength(8).WithMessage("Password should be at least 8 characters");

            RuleFor(u => u.Role)
                .NotEmpty().WithMessage("Role is required!")
                .MinimumLength(300).WithMessage("Username cannot exceed 300 characters.");
        }
    }
}
