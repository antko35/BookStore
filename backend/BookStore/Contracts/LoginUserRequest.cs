using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Contracts
{
    public record LoginUserRequest(
        
        [Required] string Email,
        [Required] string Password
        );
    public class LoginUserValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email form");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Invalid email form")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}

