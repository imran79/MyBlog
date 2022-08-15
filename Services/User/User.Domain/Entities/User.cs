﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Entities {
	public class UserEntity : IEntity {
		public Guid Id { get; set; }

		public string Name { get; set; }

		public UserType UserType { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

		public string Location { get; set; }

		public bool IsArchived { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
	}
}