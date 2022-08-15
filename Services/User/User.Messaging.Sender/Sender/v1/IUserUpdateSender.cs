using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.Entities;

namespace User.Messaging.Sender.Sender.v1
{
	public interface IUserUpdateSender
	{
		void SendUser(UserEntity user);
	}
}
