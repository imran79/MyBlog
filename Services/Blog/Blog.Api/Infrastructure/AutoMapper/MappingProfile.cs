using AutoMapper;
using Blog.Api.Models.v1;
using Blog.Data.Entities;

namespace Blog.Api.Infrastructure.AutoMapper {
    public class MappingProfile : Profile {
        public MappingProfile () {
            CreateMap<CreateBlogModel, BlogEntity> ().ForMember (x => x.Id, opt => opt.Ignore ());

            CreateMap<UpdateBlogModel, BlogEntity> ();

            CreateMap<CreatePostModel, Post> ().ForMember (x => x.Id, opt => opt.Ignore ());

            CreateMap<UpdatePostModel, Post> ();
        }
    }
}