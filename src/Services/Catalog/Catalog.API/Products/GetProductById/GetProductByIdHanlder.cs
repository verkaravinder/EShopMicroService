using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductsById
{
    public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult> { }
    public record GetProductByIdResult(Product product);
    public class GetProductByIdHanlder(IDocumentSession session, ILogger<GetProductByIdHanlder> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductQuery for ProductId: {ProductId}", query.ProductId);

            var product = await session.LoadAsync<Product>(query.ProductId, cancellationToken);

            if (product == null)
            {              
                throw new NotFoundException($"Product with ID {query.ProductId} not found.");
            }
            else
                return new GetProductByIdResult(product);

        }
    }
}
