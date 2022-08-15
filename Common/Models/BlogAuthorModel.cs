using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
	public class BlogAuthorModel
	{
		public Guid AuthorId { get; set; }
		public string AuthorName { get; set; }
		public string CurrentLocation { get; set; }
	}
}
