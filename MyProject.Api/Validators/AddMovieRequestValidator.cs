using FluentValidation;
using MyProject.Api.Dtos.RequestDtos;

namespace MyProject.Api.Validators;

public class AddMovieRequestValidator : AbstractValidator<AddMovieRequestDto>
{
    public AddMovieRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(1).WithMessage("Title must be more than 2 character")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MinimumLength(1).WithMessage("Description must be more than 2 character")
            .MaximumLength(100).WithMessage("Description cannot exceed 100 characters");
    }
}