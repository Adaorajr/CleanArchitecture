using System;

namespace CleanArchitecture.Domain.Queries.Responses.Product
{
    public class GetProductByIdResponse
    {
        public GetProductByIdResponse(Guid id, string name, string brand, decimal price, DateTime createdAt, DateTime? updatedAt)
        {
            Id = id;
            Name = name;
            Brand = brand;
            Price = price;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public Decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}