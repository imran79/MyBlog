using System;
using Blog.Api.Models.v1;
using FluentValidation;

namespace Blog.Api.Validators.v1
{
    public class UpdateBlogModelValidator : AbstractValidator<UpdateBlogModel>
    {
        public UpdateBlogModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .MinimumLength(2).
                WithMessage("The first name must be at least 2 character long");            
         
        }
    }
}