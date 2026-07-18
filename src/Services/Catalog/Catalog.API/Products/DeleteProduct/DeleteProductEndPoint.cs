using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductResponse(bool IsSucess);
    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{Id}", async (Guid Id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(Id), CancellationToken.None);
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            })
               .WithName("DeleteProduct")
               .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete a Product")
                .WithDescription("Delete a new product in the catalog.");
        }
    }
}
