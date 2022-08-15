using System.Threading;
using System;
using System.Linq;
using Blog.Data.Database;
using Blog.Data.Entities;
using Blog.Data.Repository.v1;
using Blog.Data.Test.Infrastructure;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Blog.Data.Test.Repository.v1
{
    public class RepositoryTests : DatabaseTestBase
    {
        private readonly BlogContext _blogContext;
        private readonly Repository<BlogEntity> _testee;
        private readonly Repository<BlogEntity> _testeeFake;
        private readonly BlogEntity _newBlog;

        public RepositoryTests()
        {
            _blogContext = A.Fake<BlogContext>();
            _testeeFake = new Repository<BlogEntity>(_blogContext);
            _testee = new Repository<BlogEntity>(Context);
            _newBlog = new BlogEntity
            {
                Name = "Test Blog"               
            };
        }

        [Theory]
        [InlineData("Changed")]
        public async void UpdateBlogAsync_WhenBlogIsNotNull_ShouldReturnCustomer(string name)
        {
            var blog = Context.Blogs.First();
            blog.Name = name;

            var result = await _testee.UpdateAsync(blog);

            result.Should().BeOfType<BlogEntity>();
            result.Name.Should().Be(name);
        }

        [Fact]
        public void AddAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.AddAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _blogContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.AddAsync(new BlogEntity())).Should().Throw<Exception>().WithMessage("entity could not be saved: Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public async void CreateBlogAsync_WhenBlogIsNotNull_ShouldReturnBlog()
        {
            var result = await _testee.AddAsync(_newBlog);

            result.Should().BeOfType<BlogEntity>();
        }

        [Fact]
        public async void CreateBlogAsync_WhenBlogIsNotNull_ShouldShouldAddBlog()
        {
            var CustomerCount = Context.Blogs.Count();

            await _testee.AddAsync(_newBlog);

            Context.Blogs.Count().Should().Be(CustomerCount + 1);
        }

        [Fact]
        public void GetAll_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _blogContext.Set<BlogEntity>()).Throws<Exception>();
            CancellationToken cancellationToken = new CancellationToken();
            _testeeFake.Invoking(x => x.GetAllAsync(cancellationToken)).Should().Throw<Exception>().WithMessage("Couldn't retrieve entities: Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public void UpdateAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.UpdateAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UpdateAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _blogContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.UpdateAsync(new BlogEntity())).Should().Throw<Exception>().WithMessage("entity could not be updated Exception of type 'System.Exception' was thrown.");
        }
    }
}