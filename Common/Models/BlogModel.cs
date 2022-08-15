using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
	public class BlogModel
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BlogType BlogType { get; set; }
        public List<PostModel> Posts { get; set; }     

        public Guid AuthorId { get; set; }

        public BlogAuthorModel Author { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }

    public enum BlogType
	{
        Personal,
        Technical,
        Food,
        Travel
    }
}
