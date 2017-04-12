using Microsoft.EntityFrameworkCore;
using App.Domain.Models;

namespace App.Infra.Data.Contexts
{
    public class MainContext : DbContext
    {
        private string DatabasePath { get; }

        public DbSet<User> Users { get; set; }

        public MainContext()
        {

        }

        public MainContext(string databasePath)
        {
            DatabasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }
    }
}
