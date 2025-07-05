using BuildingBlocks.Exceptions;
using Marten.Linq.QueryHandlers;
using System.Linq;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var product = await session.Query<Product>().Where(p => p.Category.Contains(query.Category)).ToListAsync(token: cancellationToken);
      
        return new GetProductByCategoryResult(product);
    }
}
