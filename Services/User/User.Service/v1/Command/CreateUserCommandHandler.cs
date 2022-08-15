using System.Threading;
using System.Threading.Tasks;
using MediatR;
using User.Data.Repository.v1;
using User.Domain;
using User.Domain.Entities;
using User.Messaging.Sender.Sender.v1;

namespace User.Service.v1.Command
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserEntity>
	{
		private readonly IUserRepository _userRespository;
		private readonly IUserUpdateSender _userUpdateSender;

		public CreateUserCommandHandler(IUserRepository userRepository, IUserUpdateSender userUpdateSender)
		{
			_userRespository = userRepository;
			_userUpdateSender = userUpdateSender;
		}

		public async Task<UserEntity> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userRespository.AddAsync(request.User);
			if (request.User.UserType == UserType.Author)
			{
				_userUpdateSender.SendUser(user);
			}
			return user;
		}
	}
}