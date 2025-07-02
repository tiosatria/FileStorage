namespace FileStorage.Abstraction.Contracts
{
    public interface IUploadStorageObject
    {
        public string DestinationPath { get; }
        public string ContentType { get; }
        public long ContentLengthBytes { get; }
        public FileVisibilityEnum Visibility { get; }
        public Stream Content { get; }
    }
}
