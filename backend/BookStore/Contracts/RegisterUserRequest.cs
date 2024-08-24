using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Contracts
{
    public record RegisterUserRequest(
        [Required] string UserName,
        [Required] string Email,
        [Required] string Password
     );

    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email form");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Invalid email form")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
