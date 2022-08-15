using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MediatR;
using User.Domain;
using User.Domain.Entities;

namespace User.Service.v1.Query
{
    public class GetAllUsersQuery : IRequest<ConfiguredCancelableAsyncEnumerable<UserEntity>>
    {
    }
}