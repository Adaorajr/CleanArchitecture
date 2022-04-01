using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using CleanArchitecture.Domain.Queries.Requests.Product;
using CleanArchitecture.Domain.Queries.Responses.Product;
using CleanArchitecture.Domain.Response;
using MediatR;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GenericCommandResult>
    {
        private readonly IUnitOfWorkContext _uow;
        public GetProductByIdHandler(IUnitOfWorkContext uow)
        {
            _uow = uow;
        }
        public async Task<GenericCommandResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _uow.ProductRepository.GetById(request.Id);

            if (result is null)
            {
                request.AddNotification("Error", "This product does not exist!");
                return new GenericCommandResult(false, "Please, check:", request.Notifications);
            }
            var response = new GetProductByIdResponse(
                            result.Id,
                            result.Name,
                            result.Brand,
                            result.Price,
                            result.CreatedAt,
                            result.UpdatedAt
                        );

            return new GenericCommandResult(true, "", response);
        }
    }
}