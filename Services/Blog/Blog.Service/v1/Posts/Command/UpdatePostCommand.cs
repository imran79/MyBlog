using Blog.Data.Entities;
using MediatR;

namespace Blog.Service.v1.Command
{
    public class UpdatePostCommand : IRequest<Post>
    {
        public Post Post { get; set; }
    }
}