using Blog.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.v1.Command
{
	public class UpsertBlogAuthorCommand : IRequest<BlogAuthor>
    {
        public BlogAuthor BlogAuthor { get; set; }
    }
}
