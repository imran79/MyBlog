using Blog.Service.v1.Command;
using Blog.Service.v1.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.v1.Services
{
	public class BlogAuthorUpdateService : IBlogAuthorUpdateService
	{
        private readonly IMediator _mediator;

        public BlogAuthorUpdateService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void UpdateBlogAuthor(UpdateBlogAuthorModel updateBlogAuthorModel)
        {
            var author = new Data.Entities.BlogAuthor { AuthorId = updateBlogAuthorModel.Id, AuthorName = updateBlogAuthorModel.Name, CurrentLocation = updateBlogAuthorModel.Location };


            try
            {
                var blogAuthor = await _mediator.Send(new UpsertBlogAuthorCommand
                {
                    BlogAuthor = author
                });

                
            }
            catch (Exception ex)
            {
                // log an error message here

                Debug.WriteLine(ex.Message);
            }
        }
    }
}
