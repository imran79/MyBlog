using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Api.Models.v1
{
	public class UpdateUserModel
	{
		[Required]
		public Guid Id { get; set; }
		[System.Runtime.Serialization.DataMember(EmitDefaultValue = false)]
		public string Name { get; set; }
		[System.Runtime.Serialization.DataMember(EmitDefaultValue = false)]
		public UserType UserType { get; set; }
		[System.Runtime.Serialization.DataMember(EmitDefaultValue = false)]

		public string Password { get; set; }

		[System.Runtime.Serialization.DataMember(EmitDefaultValue = false)]
		public string Email { get; set; }

		[System.Runtime.Serialization.DataMember(EmitDefaultValue = false)]
		public string Location { get; set; }
	}
}
