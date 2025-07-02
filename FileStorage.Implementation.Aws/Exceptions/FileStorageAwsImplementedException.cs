
using FileStorage.Core.Exceptions;

namespace FileStorage.Implementation.Aws.Exceptions
{
    internal class FileStorageAwsImplementedException(string? msg = null, Exception? innerEx = null) : FileStorageException(msg,innerEx)
    {

    }
}
