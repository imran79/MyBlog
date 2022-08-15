using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities {
    public partial class BlogEntity : IEntity {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BlogType BlogType { get; set; }
        public List<Post> Posts { get; set; }

        [ForeignKey ("Author")]

        public Guid AuthorId {get;set;}

        public BlogAuthor Author{get;set;}
        public bool IsArchived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}