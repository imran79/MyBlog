using System;
using Blog.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Test.Infrastructure
{
    public class DatabaseTestBase : IDisposable
    {
        protected readonly BlogContext Context;

        public DatabaseTestBase()
        {
            var options = new DbContextOptionsBuilder<BlogContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            Context = new BlogContext(options);

            Context.Database.EnsureCreated();

            DatabaseInitializer.Initialize(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();

            Context.Dispose();
        }
    }
}