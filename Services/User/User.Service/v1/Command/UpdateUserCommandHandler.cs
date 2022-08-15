using System.Threading;
using System.Threading.Tasks;
using MediatR;
using User.Data.Repository.v1;
using User.Domain.Entities;
using User.Messaging.Sender.Sender.v1;

namespace User.Service.v1.Command
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserEntity>
    {
        private readonly IUserRepository _userRespository;
        private readonly IUserUpdateSender _userUpdateSender;

        public UpdateUserCommandHandler(IUserRepository userRepository, IUserUpdateSender userUpdateSender)
        {
            _userRespository = userRepository;
            _userUpdateSender = userUpdateSender;
        }

        public async Task<UserEntity> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var exisitingUser = await _userRespository.GetUserByIdAsync(request.User.Id, cancellationToken);
           

            var user = await _userRespository.UpdateAsync(request.User);
            if (request.User.UserType == UserType.Author)
            {
                _userUpdateSender.SendUser(user);
            }

            return user;
        }
    }
}