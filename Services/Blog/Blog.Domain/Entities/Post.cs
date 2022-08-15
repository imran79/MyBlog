using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities {

    public partial class Post : IEntity {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [ForeignKey ("Blog")]
        public Guid BlogId { get; set; }
        public BlogEntity Blog { get; set; }

        public DateTime PublishedDate { get; set; }

        public bool IsArchived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Status Status { get; set; }
    }
}