using System.Threading;
using System.Threading.Tasks;
using MediatR;
using User.Data.Repository.v1;
using User.Domain.Entities;

namespace User.Service.v1.Query
{
	public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserEntity>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserEntity> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);
        }
    }
}