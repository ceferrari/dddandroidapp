using App.Domain.Entities;
using App.Infra.Data.Extensions;
using App.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Contexts
{
    public sealed class SqliteContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public SqliteContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreatedAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new CustomerMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}