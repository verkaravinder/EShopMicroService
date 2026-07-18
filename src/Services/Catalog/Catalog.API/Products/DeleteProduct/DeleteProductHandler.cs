using Catalog.API.Exceptions;
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id)
        : ICommand<DeleteProductResult>
    { }
    public record DeleteProductResult(bool IsSucess);
    internal class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> _logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DeleteProductCommand for Product Id: {ProductId}", command.Id);
             
            // Delete the product to the database using Marten

            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}


