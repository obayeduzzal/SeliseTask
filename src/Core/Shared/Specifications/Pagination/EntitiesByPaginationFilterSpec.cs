using Ardalis.Specification;

namespace TTM.Core.Shared.Specifications;

public class PaginationFilter
{
    public int PageSize { get; set; } = 10;
    public int CurrentPage { get; set; } = 1;
    public string[]? OrderBy { get; set; }
}

public class EntitiesByPaginationFilterSpec<T, TResult> : Specification<T, TResult>
{
    public EntitiesByPaginationFilterSpec(PaginationFilter filter)
        => Query.PaginateBy(filter);
}

public static class SpecificationBuilderExtensions
{
    public static ISpecificationBuilder<T> PaginateBy<T>(
        this ISpecificationBuilder<T> query,
        PaginationFilter filter)
    {
        if (filter.CurrentPage <= 0)
            filter.CurrentPage = 1;
        if (filter.PageSize <= 0)
            filter.PageSize = 10;
        if (filter.CurrentPage > 1)
            query = query.Skip((filter.CurrentPage - 1) * filter.PageSize);

        return query
            .Take(filter.PageSize);
    }
}