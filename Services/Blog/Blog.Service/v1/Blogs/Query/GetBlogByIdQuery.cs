using System;
using Blog.Data.Entities;
using MediatR;

namespace Blog.Service.v1.Query
{
    public class GetBlogByIdQuery : IRequest<BlogEntity>
    {
        public Guid Id { get; set; }
    }
}