using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Queries.Requests.Product;
using CleanArchitecture.Domain.Queries.Responses.Product;
using MediatR;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetById(request.Id);

            if (result is null)
            {
                throw new DomainNotFoundException("This product does not exist!");
            }

            return new GetProductByIdResponse(
                result.Id,
                result.Name,
                result.Brand,
                result.Price,
                result.CreatedAt,
                result.UpdatedAt
            );
        }
    }
}