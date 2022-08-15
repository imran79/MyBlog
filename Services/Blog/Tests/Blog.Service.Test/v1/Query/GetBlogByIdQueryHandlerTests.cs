using System;
using System.Threading.Tasks;
using Blog.Data.Entities;
using Blog.Data.Repository.v1;
using Blog.Service.v1.Query;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Blog.Service.Test.v1.Query
{
    public class GetBlogByIdQueryHandlerTests
    {
        private readonly IBlogRepository _blogRepository;
        private readonly GetBlogByIdQueryHandler _testee;
        private readonly BlogEntity _blog;
        private readonly Guid _id = Guid.Parse("803a95ef-89c5-43d5-aa2c-82a3695d9894");

        public GetBlogByIdQueryHandlerTests()
        {
            _blogRepository = A.Fake<IBlogRepository>();
            _testee = new GetBlogByIdQueryHandler(_blogRepository);

            _blog = new BlogEntity { Id= _id, Name= "Test Blog" };
        }

        [Fact]
        public async Task Handle_WithValidId_ShouldReturnCustomer()
        {
            A.CallTo(() => _blogRepository.GetBlogByIdAsync(_id, default)).Returns(_blog);

            var result = await _testee.Handle(new GetBlogByIdQuery { Id = _id }, default);

            A.CallTo(() => _blogRepository.GetBlogByIdAsync(_id, default)).MustHaveHappenedOnceExactly();
            result.Name.Should().Be("Test Blog");
        }
    }
}