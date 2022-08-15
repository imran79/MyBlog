using System;
using System.Net;
using AutoMapper;
using Blog.Api.Controllers.v1;
using Blog.Api.Models.v1;
using Blog.Data.Entities;
using Blog.Service.v1.Command;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Blog.Api.Test.Controllers.v1
{
    public class BlogControllerTests
    {
        private readonly IMediator _mediator;
        private readonly BlogController _testee;
        private readonly CreateBlogModel _createBlogModel;
        private readonly UpdateBlogModel _updateBlogModel;
        private readonly Guid _id = Guid.Parse("5224ed94-6d9c-42ec-ba93-dfb11fe68931");

        public BlogControllerTests()
        {
            var mapper = A.Fake<IMapper>();
            _mediator = A.Fake<IMediator>();
            _testee = new BlogController(mapper, _mediator);

            _createBlogModel = new CreateBlogModel
            {
                Name= "test blog"
            };
            _updateBlogModel = new UpdateBlogModel
            {
                Name = "test 1 blog"
            };
            var blog = new BlogEntity
            {
                Id = _id,
                Name= "First Blog",
                BlogType= BlogType.Technical,
            };

            A.CallTo(() => mapper.Map<BlogEntity>(A<BlogEntity>._)).Returns(blog);
            A.CallTo(() => _mediator.Send(A<CreateBlogCommand>._, default)).Returns(blog);
            A.CallTo(() => _mediator.Send(A<UpdateBlogCommand>._, default)).Returns(blog);
        }

        [Theory]
        [InlineData("CreateBlogAsync: blog is null")]
        public async void Post_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<CreateBlogCommand>._,default)).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.Blog(_createBlogModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Theory]
        [InlineData("UpdateBlogAsync: blog is null")]
        [InlineData("No blog with this id found")]
        public async void Put_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<UpdateBlogCommand>._, default)).Throws(new Exception(exceptionMessage));

            var result = await _testee.Blog(_updateBlogModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void Post_ShouldReturnCustomer()
        {
            var result = await _testee.Blog(_createBlogModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<BlogEntity>();
            result.Value.Id.Should().Be(_id);
        }

        [Fact]
        public async void Put_ShouldReturnBlog()
        {
            var result = await _testee.Blog(_updateBlogModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<BlogEntity>();
            result.Value.Id.Should().Be(_id);
        }
    }
}
