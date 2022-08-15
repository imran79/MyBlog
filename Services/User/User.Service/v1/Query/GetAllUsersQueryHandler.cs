using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using User.Data.Repository.v1;
using User.Domain;
using User.Domain.Entities;

namespace User.Service.v1.Query
{
	public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ConfiguredCancelableAsyncEnumerable<UserEntity>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

		public Task<ConfiguredCancelableAsyncEnumerable<UserEntity>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
		{
			return Task.FromResult<ConfiguredCancelableAsyncEnumerable<UserEntity>>
				( _userRepository.GetAll(cancellationToken));
		}

	}
}