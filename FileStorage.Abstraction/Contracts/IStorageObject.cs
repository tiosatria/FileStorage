namespace FileStorage.Abstraction.Contracts
{
    public interface IStorageObject : IStorageFileMetadata
    {
        public Stream Content { get; }
    }
}
