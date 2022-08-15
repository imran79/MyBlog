using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Api.Models.v1
{
	public class UserModel
	{

		public Guid Id { get; set; }

		public string Name { get; set; }

		public UserType UserType { get; set; }


		public string Email { get; set; }


		public string Location { get; set; }
	}
}
