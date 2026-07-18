
using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> _logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetProductByCategoryQuery for category: {Category}", query.Category);

            var products = await session.Query<Product>()
                                        .Where(p => p.Category.Contains(query.Category))
                                        .ToListAsync(cancellationToken);

            if (products == null || !products.Any())
            {
                _logger.LogWarning("No products found for category: {Category}", query.Category);
                throw new NotFoundException($"No products found for category: {query.Category}");
            }
            else
            {
                _logger.LogInformation("Found {Count} products for category: {Category}", products.Count, query.Category);
                return new GetProductByCategoryResult(products);
            }
        }
    }
}
