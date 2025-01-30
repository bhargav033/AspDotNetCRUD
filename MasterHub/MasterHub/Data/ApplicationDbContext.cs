using MasterHub.Model;
using Microsoft.EntityFrameworkCore;

namespace MasterHub.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlServer("Server=DESKTOP-RKAT1MH\\SQLEXPRESS01;Database=MasterHub;Integrated Security=True;TrustServerCertificate=true;Connection Timeout=60");
        }
        public DbSet<Auth> User {  get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Catagory> Catagory { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Match> Match { get; set; }
    }
}
