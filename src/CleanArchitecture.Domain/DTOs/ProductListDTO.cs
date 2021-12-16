using System;

namespace CleanArchitecture.Domain.DTOs
{
    public class ProductListDTO
    {
        public ProductListDTO(Guid id, string name, string brand, decimal price, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Brand = brand;
            Price = price;
            CreatedAt = createdAt;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public Decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}