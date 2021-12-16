using System;

namespace CleanArchitecture.Domain.InputModels
{
    public class ProductUpdateInputModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public Decimal Price { get; set; }
    }
}