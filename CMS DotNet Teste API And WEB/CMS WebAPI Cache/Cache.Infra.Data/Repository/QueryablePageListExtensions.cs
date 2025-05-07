using Cache.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cache.Infra.Data.Repository;

public static class QueryablePageListExtensions
{
    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, int indexFrom = 0, bool executeCount = true, CancellationToken cancellationToken = default)
    {
        if (indexFrom > pageIndex)
            throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");

        var count = 0;
        if (executeCount)
            count = await source.CountAsync(cancellationToken).ConfigureAwait(false);

        var items = await source
            .Skip((pageIndex - indexFrom) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var pagedList = new PagedList<T>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            IndexFrom = indexFrom,
            TotalCount = count,
            Items = items,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize),
            Source = source
        };

        return pagedList;
    }
}