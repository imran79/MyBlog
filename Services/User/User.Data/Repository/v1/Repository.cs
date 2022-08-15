using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using User.Data.Database;

namespace User.Data.Repository.v1
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly UserContext UserContext;

        public Repository(UserContext orderContext)
        {
            UserContext = orderContext;
        }

        public   ConfiguredCancelableAsyncEnumerable<TEntity> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                return  UserContext.Set<TEntity>().WithCancellation<TEntity>(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities {ex.Message}");
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await UserContext.AddAsync(entity);
                await UserContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                UserContext.Update(entity);
                await UserContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated {ex.Message}");
            }
        }

        public async Task UpdateRangeAsync(List<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateRangeAsync)} entities must not be null");
            }

            try
            {
                UserContext.UpdateRange(entities);
                await UserContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entities)} could not be updated {ex.Message}");
            }
        }

		
	}
}