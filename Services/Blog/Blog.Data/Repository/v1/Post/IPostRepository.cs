using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Entities;

namespace Blog.Data.Repository.v1
{
    public interface IPostRepository: IRepository<Post>
    {
        Task<Post> GetPostByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Post>> GetPostsByBlogIdAsync(Guid blogId, CancellationToken cancellationToken);
    }
}