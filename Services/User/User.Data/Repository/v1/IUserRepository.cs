using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Data.Repository.v1
{
	public interface IUserRepository: IRepository<UserEntity>
    {      

        Task<UserEntity> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
       
    }
}