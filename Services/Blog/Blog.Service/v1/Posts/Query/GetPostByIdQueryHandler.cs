using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Entities;
using Blog.Data.Repository.v1;
using MediatR;

namespace Blog.Service.v1.Query
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Post>
    {
        private readonly IPostRepository _postRepository;

        public GetPostByIdQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Post> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            return await _postRepository.GetPostByIdAsync(request.Id, cancellationToken);
        }
    }
}