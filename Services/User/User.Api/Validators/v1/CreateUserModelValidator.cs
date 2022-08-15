using FluentValidation;
using User.Api.Models.v1;

namespace User.Api.Validators.v1
{
	public class UserModelValidator : AbstractValidator<CreateUserModel>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("The customer name must be at least 2 character long");
            RuleFor(x => x.Name)
                .MinimumLength(2).WithMessage("The customer name must be at least 2 character long");
        }
    }
}