using Blog.Api.Validators.v1;
using FluentValidation.TestHelper;
using Xunit;

namespace Blog.Api.Test.Validator.v1
{
    public class CreateBlogModelValidatorTests
    {
        private readonly CreateBlogModelValidator _testee;

        public CreateBlogModelValidatorTests()
        {
            _testee = new CreateBlogModelValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("a")]
        public void FirstName_WhenShorterThanTwoCharacter_ShouldHaveValidationError(string name)
        {
            _testee.ShouldHaveValidationErrorFor(x => x.Name, name).WithErrorMessage("The name must be at least 2 character long");
        }

    }
}