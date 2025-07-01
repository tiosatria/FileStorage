using FileStorage.Abstraction.Contracts;

namespace FileStorage.Abstraction.Models
{
    public class StorageInfo(string storageKey, string storageName, string storageVersion, long totalSizeInBytes, long totalUseInBytes)
        : IStorageInfo
    {
        public string StorageName { get; } = storageName;
        public string StorageKey { get; set; } = storageKey;
        public string StorageVersion { get; } = storageVersion;
        public long TotalSizeAvailableInBytes { get; set; } = totalSizeInBytes;
        public long TotalUsedInBytes { get; set; } = totalUseInBytes;
    }
}
