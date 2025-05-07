using Cache.Domain.Repository;

namespace Cache.Infra.Data.Repository;

public static class EnumerablePagedListExtensions
{
    public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom = 0, bool executeCount = true) =>
        new PagedList<T>(source, pageIndex, pageSize, indexFrom, executeCount);

    public static IPagedList<TResult> ToPagedList<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int pageIndex, int pageSize, int indexFrom = 0, bool executeCount = true) =>
        new PagedList<TSource, TResult>(source, converter, pageIndex, pageSize, indexFrom, executeCount);
}