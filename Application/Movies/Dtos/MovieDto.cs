namespace MoviesApi.Application.Movies.Dtos;

//  Dto - форма даних для клієнта
public record MovieDto(
    int Id,
    string Title,
    int ReleaseYear,
    string Genre,
    double Rating
);