using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.InputModels;

namespace CleanArchitecture.Domain.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductUpdateInputModel>();
            CreateMap<ProductUpdateInputModel, Product>();
        }
    }
}