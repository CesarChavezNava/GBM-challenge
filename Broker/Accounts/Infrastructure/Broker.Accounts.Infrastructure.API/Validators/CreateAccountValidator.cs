using Broker.Accounts.Infrastructure.API.Dtos;
using FluentValidation;

namespace Broker.Accounts.Infrastructure.API.Validators;

public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
{
    public CreateAccountValidator()
    {
        RuleFor(dto => dto.Cash)
            .NotNull()
            .WithMessage("Cash is null.")
            .NotEmpty()
            .WithMessage("Cash is empty.")
            .GreaterThan(0)
            .WithMessage("Cash is too low.");
    }
}
