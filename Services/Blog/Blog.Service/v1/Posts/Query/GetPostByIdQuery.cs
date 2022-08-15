using System;
using Blog.Data.Entities;
using MediatR;

namespace Blog.Service.v1.Query
{
    public class GetPostByIdQuery : IRequest<Post>
    {
        public Guid Id { get; set; }
    }
}