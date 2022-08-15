using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repository.v1
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly BlogContext BlogContext;

        public Repository(BlogContext blogContext)
        {
            BlogContext = blogContext;
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken) 
        {
            try
            {
                return await BlogContext.Set<TEntity>().ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
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
                await BlogContext.AddAsync(entity);
                await BlogContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
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
                BlogContext.Update(entity);
                await BlogContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated {ex.Message}");
            }
        }
    }
}