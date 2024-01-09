using EzRepositories.Tests.Fixture;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzRepositories.Tests.Helpers
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<EntityWithNoId> EntityWithNoIds {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntityWithNoId>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
