using Catalog.API.Exceptions;
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>
    { }

    public record UpdateProductResult(bool IsSucess);
    public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> _logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateProductCommand for Product Id: {ProductId}", command.Id);
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if(product==null)
            {
                _logger.LogWarning("Product with Id: {ProductId} not found", command.Id);
                throw new NotFoundException($"Product not found {command.Id} ");
            }

            product.ImageFile   = command.ImageFile;
            product.Name        = command.Name;
            product.Category    = command.Category;
            product.Description = command.Description;
            product.Price       = command.Price;

            // Update the product to the database using Marten

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}

