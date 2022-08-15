using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Entities;
using Blog.Data.Repository.v1;
using MediatR;

namespace Blog.Service.v1.Query
{
    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, BlogEntity>
    {
        private readonly IBlogRepository _blogRepository;

        public GetBlogByIdQueryHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<BlogEntity> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            return await _blogRepository.GetBlogByIdAsync(request.Id, cancellationToken);
        }
    }
}