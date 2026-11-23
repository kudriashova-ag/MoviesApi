using System.Linq.Expressions;
using MoviesApi.Domain.Movies;

namespace MoviesApi.Application.Movies;

public static class MovieQueryExtensions
{
    private static readonly Dictionary<string, Expression<Func<Movie, object>>> SortColumns =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["id"] = m => m.Id,
            ["title"] = m => m.Title,
            ["year"] = m => m.ReleaseYear,
            ["genre"] = m => m.Genre,
        };

    public static bool IsAllowedSort(string? sort) =>
        string.IsNullOrWhiteSpace(sort) || SortColumns.ContainsKey(sort.Trim().TrimStart('-'));


    public static IQueryable<Movie> ApplySorting(this IQueryable<Movie> query, string? sort)
    {
        if (string.IsNullOrWhiteSpace(sort))
        {
            return query.OrderBy(m => m.Id);
        }

        var value = sort.Trim();
        var descending = value.StartsWith('-');
        var column = SortColumns[value.TrimStart('-')];

        var ordered = descending ? query.OrderByDescending(column) : query.OrderBy(column);
        return ordered.ThenBy(m => m.Id);
    }


    public static IQueryable<Movie> ApplyFilters(this IQueryable<Movie> query, MovieQueryParameters parameters)
    {
        if (!string.IsNullOrWhiteSpace(parameters.Genre))
        {
            var genre =  parameters.Genre.Trim();
            query = query.Where(m => m.Genre == genre);
        }

        if (parameters.YearFrom is not null)
        {
            var yearFrom = parameters.YearFrom.Value;
            query = query.Where(m => m.ReleaseYear >= yearFrom);
        }

        if (parameters.YearTo is not null)
        {
            var yearTo = parameters.YearTo.Value;
            query = query.Where(m => m.ReleaseYear <= yearTo);
        } 

        if (!string.IsNullOrWhiteSpace(parameters.Search))
        {
            var search = parameters.Search.Trim();
            query = query.Where(m => m.Title.Contains(search));
        }

        return query;
    }

}