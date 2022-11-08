using IT_Consulting_CRM_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IT_Consulting_CRM_API.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Requests> Request { get; set; }
        public DbSet<Blogs> Blog { get; set; }
        public DbSet<Contacts> Contact { get; set; }
        public DbSet<Projects> Project { get; set; }
        public DbSet<Services> Service { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var options = optionsBuilder.UseSqlServer(connectionString)
            .Options;
        }
    }
}
