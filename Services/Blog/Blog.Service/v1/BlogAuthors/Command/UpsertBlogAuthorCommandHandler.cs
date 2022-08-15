using Blog.Data.Entities;
using Blog.Data.Repository.v1;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Service.v1.Command
{
	public class UpsertBlogAuthorCommandHandler : IRequestHandler<UpsertBlogAuthorCommand, BlogAuthor>
	{

		private readonly IBlogAuthorRepository _blogAuthorRepository;

		public UpsertBlogAuthorCommandHandler(IBlogAuthorRepository blogAuthorRepository)
		{
			_blogAuthorRepository = blogAuthorRepository;
		}
		public async Task<BlogAuthor> Handle(UpsertBlogAuthorCommand request, CancellationToken cancellationToken)
		{
			return await _blogAuthorRepository.Upsert(request.BlogAuthor, cancellationToken);
		}
	}
}
