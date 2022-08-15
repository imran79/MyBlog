using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Entities;
using Blog.Data.Repository.v1;
using Blog.Service.v1.Blogs.Query;
using MediatR;

namespace Blog.Service.v1.Query
{
    public class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, List<BlogEntity>>
    {
        private readonly IBlogRepository _blogRepository;

        public GetBlogsQueryHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

    

        public async Task<List<BlogEntity>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            return await _blogRepository.GetAllWithBlogAuthor(cancellationToken);
        }
    }
}