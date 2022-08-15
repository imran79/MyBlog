using System;
using System.Collections.Generic;
using Blog.Data.Entities;
using MediatR;

namespace Blog.Service.v1.Query
{
    public class GetPostsByBlogIdQuery : IRequest<List<Post>>
    {
        public Guid BlogId { get; set; }
    }
}