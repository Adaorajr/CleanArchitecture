using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infra.Mappings
{
    public class ProductMapping
    {
        public ProductMapping(EntityTypeBuilder<Product> entityBuilder)
        {
            entityBuilder.ToTable("Products");
            entityBuilder.HasKey(p => p.Id).HasName("Pk_Products_Id");
            entityBuilder.Property(p => p.Name).HasColumnType("VARCHAR(100)").IsRequired();
            entityBuilder.Property(p => p.Brand).HasColumnType("VARCHAR(50)").IsRequired();
            entityBuilder.Property(p => p.Price).HasColumnType("DECIMAL(4,2)").IsRequired();
        }
    }
}