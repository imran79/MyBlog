using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
	public class PostModel
	{
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

       
        public Guid BlogId { get; set; }
        public BlogModel Blog { get; set; }

        public DateTime PublishedDate { get; set; }

        public bool IsArchived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Status Status { get; set; }
    }

    public enum Status
    {
        Published,
        Draft,
        Archived

    }
}
