namespace MoviesApi.Application.Files;


public class FileValidator
{
    /// <summary>Повертає текст помилки або null, якщо файл підходить.</summary>
    public static async Task<string?> ValidateAsync(UploadedFile file, FileRules rules)
    {
        // 1. Розмір. Найдешевша перевірка — першою.
        if (file.Length == 0)
            return "Файл порожній.";

        if (file.Length > rules.MaxBytes)
            return $"Файл завеликий: максимум {rules.MaxBytes / 1024 / 1024} МБ.";

        // 2. Розширення — лише з білої скриньки (whitelist).
        // Path.GetExtension бере ОСТАННЄ розширення: "poster.jpg.exe" → ".exe" → відмова.
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!rules.Signatures.TryGetValue(extension, out var signature))
            return $"Недозволений тип файлу. Дозволено: {string.Join(", ", rules.Signatures.Keys)}.";

        // 3. Вміст — «магічні байти». Перейменувати virus.exe на virus.jpg легко, а підробити
        // перші байти так, щоб файл лишився робочою програмою, — ні.
        var header = new byte[signature.Length];
        var read = await file.Content.ReadAtLeastAsync(header, header.Length, throwOnEndOfStream: false);

        // 🔎 Повертаємося на початок потоку: далі файл зберігатимуть повністю, з першого байта.
        file.Content.Position = 0;

        if (read < signature.Length || !header.AsSpan().SequenceEqual(signature))
            return $"Вміст файлу не відповідає формату {extension}.";

        return null;
    }

}