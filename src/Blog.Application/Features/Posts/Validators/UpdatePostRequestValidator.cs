using Blog.Application.Features.Posts.Dtos;
using FluentValidation;

namespace Blog.Application.Features.Posts.Validators
{
    public class UpdatePostRequestValidator : AbstractValidator<UpdatePostRequest>
    {
        public UpdatePostRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Content)
                .NotEmpty();
        }
    }
}