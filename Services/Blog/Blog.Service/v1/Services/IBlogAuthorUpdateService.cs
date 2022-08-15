using Blog.Service.v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.v1.Services
{
    public interface IBlogAuthorUpdateService
    {
        void UpdateBlogAuthor(UpdateBlogAuthorModel updateBlogAuthorModel);
    }
}
