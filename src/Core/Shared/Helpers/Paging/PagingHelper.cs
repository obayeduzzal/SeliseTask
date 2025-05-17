namespace TTM.Core.Shared.Helpers;
public static class PagingHelper
{
    public static string GetDataRange(int pageSize, int currentPage) =>
        pageSize == -1 ? "LIMIT All" : $"OFFSET {pageSize * ((currentPage <= 0 ? 1 : currentPage) - 1)} LIMIT {pageSize}";
}
