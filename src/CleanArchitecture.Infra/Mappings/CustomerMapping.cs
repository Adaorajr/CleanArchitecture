using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infra.Mappings
{
    public class CustomerMapping
    {
        public CustomerMapping(EntityTypeBuilder<Customer> entityBuilder)
        {
            entityBuilder.ToTable("Customers");
            entityBuilder.HasKey(p => p.Id).HasName("Pk_Customers_Id");
            entityBuilder.Property(p => p.Name).HasColumnType("VARCHAR(200)").IsRequired();
        }
    }
}
