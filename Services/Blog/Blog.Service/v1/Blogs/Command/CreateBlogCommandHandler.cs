using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Entities;
using Blog.Data.Repository.v1;
using MediatR;

namespace Blog.Service.v1.Command
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, BlogEntity>
    {
        private readonly IBlogRepository _blogRepository;

        public CreateBlogCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<BlogEntity> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            return await _blogRepository.AddAsync(request.Blog);
        }
    }
}