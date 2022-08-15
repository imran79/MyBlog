using System;
using AutoMapper;
using Blog.Api.Infrastructure.AutoMapper;
using Blog.Api.Models.v1;
using Blog.Data.Entities;
using FluentAssertions;
using Xunit;

namespace Blog.Test.Infrastructure.AutoMapper
{
    public class MappingProfileTests
    {
        private readonly CreateBlogModel _createBlogModel;
        private readonly UpdateBlogModel _updateBlogModel;
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mockMapper.CreateMapper();

            _createBlogModel = new CreateBlogModel
            {
               Name= "Test 1"
            };
            _updateBlogModel = new UpdateBlogModel
            {
                Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                Name= "test 2"
            };
        }

        [Fact]
        public void Map_Customer_CreateBlogModel_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<BlogEntity, CreateBlogModel>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_Customer_UpdateBlogModel_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<BlogEntity, UpdateBlogModel>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_Blog_Blog_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<BlogEntity, BlogEntity>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_CreateBlogModel_Blog()
        {
            var blog = _mapper.Map<BlogEntity>(_createBlogModel);

            blog.Name.Should().Be(_createBlogModel.Name);
        }
          

        [Fact]
        public void Map_UpdateBlogModel_Blog()
        {
            var customer = _mapper.Map<BlogEntity>(_updateBlogModel);

            customer.Id.Should().Be(_updateBlogModel.Id);
            customer.Name.Should().Be(_createBlogModel.Name);
            
        }
    }
}