using System.Collections.Generic;
using Blog.Data.Entities;
using MediatR;

namespace Blog.Service.v1.Blogs.Query {
    public class GetBlogsQuery: IRequest<List<BlogEntity>> {

    }
}