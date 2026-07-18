
namespace Catalog.API.Products.GetProductsById
{
    public record GetProductByIdResponse(Product product);
    public class GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (ISender sender, Guid id) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id), CancellationToken.None);
                var response = result.Adapt<GetProductByIdResponse>();

                if(response == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithDescription("Retrieves a product from the catalog by its unique identifier.");
        }
    }
}

