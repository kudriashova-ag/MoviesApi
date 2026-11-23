using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Application.Movies;

public class MovieQueryParameters : IValidatableObject
{
    [Range(1, 10_000)]
    public int Page { get; set; } = 1;

    [Range(1, 50)]
    public int PageSize { get; set; } = 2;

    public string? Sort { get; set; }

    [StringLength(50)]
    public string? Genre { get; set; } = string.Empty;

    [Range(1888, 2100)]
    public int? YearFrom { get; set; }

    [Range(1888, 2100)]
    public int? YearTo { get; set; }

    [StringLength(50)]
    public string? Search { get; set; } = string.Empty;




    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!MovieQueryExtensions.IsAllowedSort(Sort))
        {
            yield return new ValidationResult($"Невідоме поле сортування '{Sort}'.");
        }
    }
}