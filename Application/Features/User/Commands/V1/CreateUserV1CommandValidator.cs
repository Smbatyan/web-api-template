
using FluentValidation;

namespace Application.Features.User.Commands.V1;

public class CreateUserV1CommandValidator : AbstractValidator<CreateUserV1Command>
{
    public CreateUserV1CommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();
    }
}