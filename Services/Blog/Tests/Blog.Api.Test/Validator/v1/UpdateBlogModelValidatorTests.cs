using Blog.Api.Validators.v1;
using FluentValidation.TestHelper;
using Xunit;

namespace Blog.Api.Test.Validator.v1
{
    public class UpdateBlogModelValidatorTests
    {
        private readonly UpdateBlogModelValidator _testee;

        public UpdateBlogModelValidatorTests()
        {
            _testee = new UpdateBlogModelValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        public void Name_WhenShorterThanTwoCharacter_ShouldHaveValidationError(string name)
        {
            _testee.ShouldHaveValidationErrorFor(x => x.Name, name).WithErrorMessage("The  name must be at least 2 character long");
        }

        

       
    }
}