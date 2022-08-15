using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.v1.Models
{
	public class UpdateBlogAuthorModel
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Location { get; set; }
	}
}
