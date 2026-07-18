
using Catalog.API.Products.GetProductsById;

namespace Catalog.API.Products.GetProductByCategory
{

    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (ISender sender, string category) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category), CancellationToken.None);
                var response = result.Adapt<GetProductByCategoryResponse>();

                if (response == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(response);
            })
             .WithName("GetProductByCategory")
             .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Get Product By Category")
             .WithDescription("Retrieves products from the catalog by their category.");
        }
    }
}
