using FileStorage.Abstraction.Events;

namespace FileStorage.Abstraction.Contracts
{
    public interface IFileStorageService
    {
        public string StorageKey { get; }

        Task<IFileUploadResult> UploadAsync(IUploadStorageObject uploadStorageObject, CancellationToken cancellationToken = default);

        Task<IStorageObject> DownloadAsync(string path, EventHandler<StorageTransferProgressArgs>? onDownloadProgressChanged = null, CancellationToken cancellationToken = default);
        
        Task<bool> DeleteAsync(string path, CancellationToken cancellationToken = default);
        
        Task<bool> IsExistAsync(string path, CancellationToken cancellationToken = default);
        
        Task<IStorageFileMetadata> GetFileMetadataAsync(string path, CancellationToken cancellationToken = default);
        
        Task<IStorageInfo> GetStorageInfoAsync(CancellationToken cancellationToken = default);

    }
}
