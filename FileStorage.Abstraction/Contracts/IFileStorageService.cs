using FileStorage.Abstraction.Events;

namespace FileStorage.Abstraction.Contracts
{
    public interface IFileStorageService
    {
        public string StorageKey { get; }

        Task<IFileUploadResult> UploadAsync(IUploadStorageObject uploadStorageObject,
            EventHandler<StorageTransferProgressArgs>? uploadProgressEvent = null,
            CancellationToken cancellationToken = default);

        Task<IStorageObject> DownloadAsync(string path, EventHandler<StorageTransferProgressArgs>? downloadProgressEvent = null, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(string path, CancellationToken cancellationToken = default);

        Task<bool> IsExistAsync(string path, CancellationToken cancellationToken = default);

        Task<IStorageFileMetadata> GetFileMetadataAsync(string path, CancellationToken cancellationToken = default);

        Task<IStorageInfo> GetStorageInfoAsync(CancellationToken cancellationToken = default);

    }
}
