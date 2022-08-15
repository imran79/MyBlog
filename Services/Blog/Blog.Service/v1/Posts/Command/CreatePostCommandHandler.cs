using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Entities;
using Blog.Data.Repository.v1;
using MediatR;

namespace Blog.Service.v1.Command
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Post>
    {
        private readonly IPostRepository _postRepository;

        public CreatePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            return await _postRepository.AddAsync(request.Post);
        }
    }
}