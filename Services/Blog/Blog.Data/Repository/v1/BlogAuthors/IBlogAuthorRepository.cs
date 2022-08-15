using Blog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Data.Repository.v1
{
	public interface IBlogAuthorRepository : IRepository<BlogAuthor>
	{
		Task<BlogAuthor> Upsert(BlogAuthor blogAuthor, CancellationToken cancellationToken);
	}
}
