using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Entities;
using Blog.Data.Repository.v1;
//using Blog.Messaging.Receive.Sender.v1;
using MediatR;

namespace Blog.Service.v1.Command
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, BlogEntity>
    {
        private readonly IBlogRepository _blogRepository;      

        public UpdateBlogCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<BlogEntity> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.UpdateAsync(request.Blog);         
            return blog;
        }
    }
}