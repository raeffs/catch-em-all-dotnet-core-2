using System;
using Microsoft.EntityFrameworkCore;

namespace Raefftec.CatchEmAll.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Query> Queries { get; set; }

        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(x => new { x.UserId, x.Number })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public object NoTracking<T>()
        {
            throw new NotImplementedException();
        }
    }
}
