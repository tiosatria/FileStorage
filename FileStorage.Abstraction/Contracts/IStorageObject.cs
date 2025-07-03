namespace FileStorage.Abstraction.Contracts
{
    public interface IStorageObject : IStorageFileMetadata,IDisposable,IAsyncDisposable
    {
        public Stream Content { get; }
    }
}
