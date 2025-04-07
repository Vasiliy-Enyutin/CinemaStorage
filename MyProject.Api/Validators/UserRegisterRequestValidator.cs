using FluentValidation;
using MyProject.Core.Dtos.RequestDtos;

namespace MyProject.Api.Validators;

public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequestDto>
{
    public UserRegisterRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(4).WithMessage("Username must be more than 4 characters")
            .MaximumLength(20).WithMessage("Username cannot exceed 20 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(4).WithMessage("Password must be more than 4 characters")
            .MaximumLength(20).WithMessage("Password cannot exceed 20 characters");
    }
}