using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Queries.Requests.Product;
using CleanArchitecture.Domain.Queries.Responses.Product;
using MediatR;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<GetProductByIdResponse>>
    {

        private readonly IProductRepository _productRepository;
        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<GetProductByIdResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetAll();
            var response = result.Select(p => new GetProductByIdResponse(p.Id, p.Name, p.Brand, p.Price, p.CreatedAt, p.UpdatedAt)).ToList();
            return response;
        }
    }
}