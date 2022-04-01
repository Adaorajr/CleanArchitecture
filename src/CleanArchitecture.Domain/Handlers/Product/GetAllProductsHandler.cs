using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using CleanArchitecture.Domain.Queries.Requests.Product;
using CleanArchitecture.Domain.Queries.Responses.Product;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<GetProductByIdResponse>>
    {

        private readonly IUnitOfWorkContext _uow;
        public GetAllProductsHandler(IUnitOfWorkContext uow)
        {
            _uow = uow;
        }
        public async Task<List<GetProductByIdResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await _uow.ProductRepository.GetAll();
            var response = result.Select(p =>
            new GetProductByIdResponse(
                p.Id,
                p.Name,
                p.Brand,
                p.Price,
                p.CreatedAt,
                p.UpdatedAt)).ToList();
            return response;
        }
    }
}