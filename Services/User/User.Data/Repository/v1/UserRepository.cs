using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Data.Database;
using User.Domain.Entities;

namespace User.Data.Repository.v1
{
	public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(UserContext orderContext) : base(orderContext)
        {
        }

        
        public async Task<UserEntity> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await UserContext.User.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        }

		
	}
}