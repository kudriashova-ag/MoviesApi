namespace MoviesApi.Application.Common;

public class PagedResult<T>
{
    public PagedResult(IReadOnlyList<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Список елементів
    /// </summary>
    public IReadOnlyList<T> Items { get; }
    /// <summary>
    /// Номер сторінки
    /// </summary>
    public int Page { get; }
    /// <summary>
    /// Скількі на одній сторінці
    /// </summary>
    public int PageSize { get; }
    /// <summary>
    /// Кількість всіх елементів
    /// </summary>
    public int TotalCount { get; }

    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}