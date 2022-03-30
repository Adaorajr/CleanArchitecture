using System;

namespace CleanArchitecture.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product(string name, string brand, decimal price, DateTime createdAt)
        {
            Name = name;
            Brand = brand;
            Price = price;
            CreatedAt = createdAt;
        }

        public string Name { get; private set; }
        public string Brand { get; private set; }
        public decimal Price { get; private set; }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeBrand(string brand)
        {
            Brand = brand;
        }

        public void ChangePrice(decimal price)
        {
            Price = price;
        }
    }
}