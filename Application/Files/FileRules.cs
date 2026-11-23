namespace MoviesApi.Application.Files;


public class FileRules
{
    public required long MaxBytes { get; init; }
    public required IReadOnlyDictionary<string, byte[]> Signatures { get; init; }

    public static readonly FileRules Poster = new()
    {
        MaxBytes = 2 * 1024 * 1024,
        Signatures = new Dictionary<string, byte[]>
        {
            [".jpg"] = [0xFF, 0xD8, 0xFF],
            [".jpeg"] = [0xFF, 0xD8, 0xFF],
            [".png"] = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A],
        },
    };

    public static readonly FileRules Script = new()
    {
        MaxBytes = 10 * 1024 * 1024,
        Signatures = new Dictionary<string, byte[]>
        {
            [".pdf"] = "%PDF-"u8.ToArray(),
        },
    };
}