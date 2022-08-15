using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace User.Data.Repository.v1
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
         ConfiguredCancelableAsyncEnumerable<TEntity> GetAll(CancellationToken cancellationToken);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task UpdateRangeAsync(List<TEntity> entities);
    }
}