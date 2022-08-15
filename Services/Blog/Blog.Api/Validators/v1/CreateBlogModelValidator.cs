using System;
using Blog.Api.Models.v1;
using FluentValidation;

namespace Blog.Api.Validators.v1
{
    public class CreateBlogModelValidator : AbstractValidator<CreateBlogModel>
    {
        public CreateBlogModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("The  name must be at least 2 character long");          
        }
    }
}