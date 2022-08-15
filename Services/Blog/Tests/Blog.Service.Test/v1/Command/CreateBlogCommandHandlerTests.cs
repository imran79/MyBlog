using Blog.Data.Entities;
using Blog.Data.Repository.v1;
using Blog.Service.v1.Command;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Blog.Service.Test.v1.Command
{
    public class CreateBlogCommandHandlerTests
    {
        private readonly CreateBlogCommandHandler _testee;
        private readonly IBlogRepository _blogRepository;

        public CreateBlogCommandHandlerTests()
        {
            _blogRepository = A.Fake<IBlogRepository>();
            _testee = new CreateBlogCommandHandler(_blogRepository);
        }

        [Fact]
        public async void Handle_ShouldCallAddAsync()
        {
            await _testee.Handle(new CreateBlogCommand(), default);

            A.CallTo(() => _blogRepository.AddAsync(A<BlogEntity>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Handle_ShouldReturnCreatedCustomer()
        {
            A.CallTo(() => _blogRepository.AddAsync(A<BlogEntity>._)).Returns(new BlogEntity
            {
                Name = "Yoda"
            });

            var result = await _testee.Handle(new CreateBlogCommand(), default);

            result.Should().BeOfType<BlogEntity>();
            result.Name.Should().Be("Yoda");
        }
    }
}