using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Entities;

namespace Blog.Data.Repository.v1
{
    public interface IBlogRepository: IRepository<BlogEntity>
    {
        Task<BlogEntity> GetBlogByIdAsync(Guid id, CancellationToken cancellationToken);

         Task<List<BlogEntity>> GetAllWithBlogAuthor(CancellationToken cancellationToken);
    }
}