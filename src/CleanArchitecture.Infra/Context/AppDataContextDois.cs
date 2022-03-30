using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Infra.Context
{
    public class AppDataContextDois : DbContext
    {
        public IConfiguration Configuration { get; }
        public AppDataContextDois()
        {
        }

        public AppDataContextDois(DbContextOptions<AppDataContextDois> options, IConfiguration configuration)
        : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite(Configuration.GetConnectionString("SqliteConStringDois"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Mappings
            new CustomerMapping(modelBuilder.Entity<Customer>());
        }

        public DbSet<Customer> Customers { get; set; }
    }
}