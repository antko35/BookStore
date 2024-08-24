using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Contracts
{
    public record BooksRequest(
        
        string Title,
        string Description,
        decimal Price);

    public class BookRequestValidator : AbstractValidator<BooksRequest>
    {
        public BookRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.")
                .PrecisionScale(2, 18, true).WithMessage("Price cannot have more than 2 decimal places.");
        }
    }
}
