using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Data.Repository.v1
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}