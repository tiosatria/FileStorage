
namespace FileStorage.Abstraction.Contracts
{
    public interface IStorageFileMetadata
    {
        public string FilePath { get; }
        public string FileName { get; }
        public FileVisibilityEnum Visibility { get; }
        public string ContentType { get; }
        public long ContentLengthBytesBytes { get; }
        public string? FileExtension { get;  }
        public DateTime? LastModifiedUtc { get; }
        public DateTime? CreatedAtUtc { get; }
    }
}
