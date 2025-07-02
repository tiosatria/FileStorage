
namespace FileStorage.Core.Exceptions
{
    public class FileStorageException(string? msg = null, Exception? ex = null) : Exception(msg, ex);
}
