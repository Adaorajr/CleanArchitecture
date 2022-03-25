using CleanArchitecture.Domain.InputModels;
using FluentValidation;

namespace CleanArchitecture.Api.Validators
{
    public class ProductUpdateInputModelValidator : AbstractValidator<ProductUpdateInputModel>
    {
        public ProductUpdateInputModelValidator()
        {
            RuleFor(p => p.Name)
            .Length(5, 100)
            .WithMessage("The name must be between 5 and 100 caracters!")
            .NotEmpty()
            .WithMessage("The name can't be null or empty!");

            RuleFor(p => p.Brand)
            .Length(2, 50)
            .WithMessage("The brand must be between 2 and 50 caracters!")
            .NotEmpty()
            .WithMessage("The brand can't be null or empty!");

            RuleFor(p => p.Price)
            .NotEmpty()
            .WithMessage("The price can't be null or empty!");
        }
    }
}