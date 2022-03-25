using System;

namespace CleanArchitecture.Domain.InputModels
{
    public class ProductCreateInputModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public Decimal Price { get; set; }
    }
}