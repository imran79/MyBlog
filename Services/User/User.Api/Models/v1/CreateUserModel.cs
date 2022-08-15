using System;
using System.ComponentModel.DataAnnotations;
using User.Domain.Entities;

namespace User.Api.Models.v1
{
    public class CreateUserModel
    {	
		[Required]
		public string Name { get; set; }

		public UserType UserType { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Location { get; set; }
	}
}