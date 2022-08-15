using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities
{
    public class BlogAuthor {   
       
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string CurrentLocation { get; set; }
    }
}