namespace TTM.Core.Shared.DTOs;
public class PagedData<T>
{
    public required List<T> Data { get; set; }
    public required PagedMeta Meta { get; set; }
}

public class PagedModelData<T>
{
    public List<T> Data { get; set; } = [];
    public long TotalCount { get; set; } = 0;
}

public class PagedMeta
{
    public long TotalCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public long TotalPage { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}

public class Paging
{
    public int CurrentPage { get; private set; }
    public long TotalPage { get; private set; }
    public int PageSize { get; private set; }
    public long TotalCount { get; private set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPage;

    public Paging(long totalCount, int currentPgae, int pageSize)
    {
        TotalCount = totalCount;
        PageSize = pageSize;
        CurrentPage = currentPgae <= 0 ? 1 : currentPgae;
        TotalPage = pageSize == -1 ? 1 : (long)Math.Ceiling(totalCount / (double)pageSize);
    }

    public PagedMeta Meta()
    {
        return new PagedMeta
        {
            TotalCount = TotalCount,
            PageSize = PageSize,
            CurrentPage = CurrentPage,
            TotalPage = TotalPage,
            HasPreviousPage = HasPreviousPage,
            HasNextPage = HasNextPage,
        };
    }
}