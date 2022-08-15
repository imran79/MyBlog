using Blog.Data.Entities;
using Blog.Data.Repository.v1;
// using Blog.Messaging.Receive.Sender.v1;
using Blog.Service.v1.Command;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Blog.Service.Test.v1.Command
{
    public class UpdateBlogCommandHandlerTests
    {
        private readonly UpdateBlogCommandHandler _testee;
        private readonly IBlogRepository _blogRepository;
       //  private readonly IBlogUpdateSender _customerUpdateSender;
        private readonly BlogEntity _blog;

        public UpdateBlogCommandHandlerTests()
        {
            _blogRepository = A.Fake<IBlogRepository>();            
            _testee = new UpdateBlogCommandHandler(_blogRepository);

            _blog = new BlogEntity
            {
                Name = "Yoda"
            };
        }

        [Fact]
        public async void Handle_ShouldCallCustomerUpdaterSenderSendCustomer()
        {
            A.CallTo(() => _blogRepository.UpdateAsync(A<BlogEntity>._)).Returns(_blog);

            await _testee.Handle(new UpdateBlogCommand(), default);

          
        }

        [Fact]
        public async void Handle_ShouldReturnUpdatedCustomer()
        {
            A.CallTo(() => _blogRepository.UpdateAsync(A<BlogEntity>._)).Returns(_blog);

            var result = await _testee.Handle(new UpdateBlogCommand(), default);

            result.Should().BeOfType<BlogEntity>();
            result.Name.Should().Be(_blog.Name);
        }

        [Fact]
        public async void Handle_ShouldUpdateAsync()
        {
            await _testee.Handle(new UpdateBlogCommand(), default);

            A.CallTo(() => _blogRepository.UpdateAsync(A<BlogEntity>._)).MustHaveHappenedOnceExactly();
        }
    }
}