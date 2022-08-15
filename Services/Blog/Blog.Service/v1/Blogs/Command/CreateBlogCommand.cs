using Blog.Data.Entities;
using MediatR;

namespace Blog.Service.v1.Command
{
    public class CreateBlogCommand : IRequest<BlogEntity>
    {
        public BlogEntity Blog { get; set; }
    }
}