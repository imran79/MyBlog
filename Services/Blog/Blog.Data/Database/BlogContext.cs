using System;
using System.Collections.Generic;
using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Database {
    public class BlogContext : DbContext {
        public BlogContext () { }

        public BlogContext (DbContextOptions<BlogContext> options) : base (options) {

           
        }
        public DbSet<BlogEntity> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<BlogAuthor> BlogAuthors { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
           

            modelBuilder.Entity<BlogEntity> (entity => {
               entity.Property (e => e.Id).HasDefaultValueSql ("(newid())");              
            });

             modelBuilder.Entity<Post> (entity => {
               entity.Property (e => e.Id).HasDefaultValueSql ("(newid())");              
            });
            modelBuilder.Entity<BlogAuthor>(entity =>
            {
                entity.HasKey(e => e.AuthorId);              
            });
        }
    }
}