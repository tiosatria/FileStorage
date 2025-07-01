namespace FileStorage.Abstraction.Contracts
{
    public interface IStorageObject
    {
        public Stream Content { get; }
        public string ContentType { get; }
        public string FileName { get; }
        public FileVisibilityEnum Visibility { get; }
    }
}
