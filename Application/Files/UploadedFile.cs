namespace MoviesApi.Application.Files;

public sealed record UploadedFile(Stream Content, string FileName, long Length);