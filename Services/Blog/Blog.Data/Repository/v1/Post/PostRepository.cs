using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Database;
using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Blog.Data.Repository.v1
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(BlogContext blogContext) : base(blogContext)
        {
        }

        public async Task<Post> GetPostByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await BlogContext.Posts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<Post>> GetPostsByBlogIdAsync(Guid blogId, CancellationToken cancellationToken){
            return await BlogContext.Posts.Where(x => x.BlogId == blogId).ToListAsync(cancellationToken);
        }
    }
}