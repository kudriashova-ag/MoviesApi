using Microsoft.EntityFrameworkCore;
using MoviesApi.Application.Common;

namespace MoviesApi.Infrastructure.Persistence;

public static class PaginationExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int page,
        int pageSize
    )
    {
        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>(items, page, pageSize, totalCount);
    }
}