using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Data.Database;
using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repository.v1
{
    public class BlogRepository : Repository<BlogEntity>, IBlogRepository
    {
        public BlogRepository(BlogContext blogContext) : base(blogContext)
        {
        }

        public async Task<List<BlogEntity>> GetAllWithBlogAuthor(CancellationToken cancellationToken)
        {
          return await BlogContext.Blogs.Include(x => x.Author).ToListAsync();
        }

        public async Task<BlogEntity> GetBlogByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await BlogContext.Blogs.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        
    }
}