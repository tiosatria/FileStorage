namespace FileStorage.Abstraction.Contracts
{
    public interface IStorageInfo
    {
        public string StorageName { get; }
        public string StorageKey { get;  }
        public string StorageVersion { get; }
        public long TotalSizeAvailableInBytes { get; }
        public long TotalUsedInBytes { get; }
    }
}
