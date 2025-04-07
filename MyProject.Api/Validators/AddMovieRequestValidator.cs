using FluentValidation;
using MyProject.Api.Dtos.RequestDtos;
using MyProject.Core.Dtos.RequestDtos;

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
            .MinimumLength(1).WithMessage("Description must be more than 2 character")
            .MaximumLength(100).WithMessage("Description cannot exceed 100 characters");

        RuleFor(x => x.Assessment)
            .InclusiveBetween(0, 10)
            .WithMessage("Assessment must be between 0 and 10");
        
        RuleFor(x => x.AssessmentKinopoisk)
            .InclusiveBetween(0, 10)
            .WithMessage("Assessment Kinopoisk must be between 0 and 10");
        
        RuleFor(x => x.Length)
            .GreaterThan(0)
            .WithMessage("Movie length must be greater than 0");
    }
}