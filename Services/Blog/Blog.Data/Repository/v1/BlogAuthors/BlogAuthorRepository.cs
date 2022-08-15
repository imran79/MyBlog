using Blog.Data.Database;
using Blog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Data.Repository.v1
{
	public class BlogAuthorRepository : Repository<BlogAuthor>, IBlogAuthorRepository
	{
		public BlogAuthorRepository(BlogContext blogContext) : base(blogContext)
		{
		}
		public async Task<BlogAuthor> Upsert(BlogAuthor blogAuthor, CancellationToken cancellationToken)
		{
			var author = BlogContext.BlogAuthors.FirstOrDefault<BlogAuthor>(c => c.AuthorId == blogAuthor.AuthorId);		
			if(author != null)
			{
				BlogContext.Entry(author).CurrentValues.SetValues(blogAuthor);
				 await BlogContext.SaveChangesAsync();
				return blogAuthor;
			}
			else
			{
				return await AddAsync(blogAuthor);
			}
		}
	}
}
