
namespace FileStorage.Abstraction.Contracts
{
    public interface IStorageFileMetadata
    {
        public string FileName { get; }
        public FileVisibilityEnum Visibility { get; }
        public string ContentType { get; }
        public long ContentLengthBytes { get; }
    }
}
