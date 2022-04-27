using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.DTO.Product
{
    public class ProductDTO
    {
        public ProductDTO()
        {

        }
        public ProductDTO(string id, string name, string brand, decimal price, string createdAt, string updatedAt)
        {
            Id = id;
            Name = name;
            Brand = brand;
            Price = price;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
