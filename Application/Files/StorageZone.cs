namespace MoviesApi.Application.Files;

public enum StorageZone
{
    /// <summary> Віддаються напряму з URL </summary>
    Public,
    /// <summary> віддаються лише через контролер </summary>
    Private
}