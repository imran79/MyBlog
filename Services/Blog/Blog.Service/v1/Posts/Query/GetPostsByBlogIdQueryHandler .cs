using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Entities;
using Blog.Data.Repository.v1;
using MediatR;

namespace Blog.Service.v1.Query
{
    public class GetPostsByBlogIdQueryHandler : IRequestHandler<GetPostsByBlogIdQuery, List<Post>>
    {
        private readonly IPostRepository _postRepository;

        public GetPostsByBlogIdQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<Post>> Handle(GetPostsByBlogIdQuery request, CancellationToken cancellationToken)
        {
            return await _postRepository.GetPostsByBlogIdAsync(request.BlogId, cancellationToken);
        }

      
    }
}