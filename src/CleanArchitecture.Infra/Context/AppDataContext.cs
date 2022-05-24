using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Infra.Context
{
    public class AppDataContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public AppDataContext()
        {
        }
        public AppDataContext(DbContextOptions<AppDataContext> options, IConfiguration configuration)
        : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Mappings
            new ProductMapping(modelBuilder.Entity<Product>());
        }

        public DbSet<Product> Produtcs { get; set; }
    }
}