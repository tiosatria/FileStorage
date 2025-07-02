
using Amazon.S3.Model;
using FileStorage.Abstraction.Contracts;
using FileStorage.Core.Exceptions;

namespace FileStorage.Implementation.Aws.Models
{
    public class S3UploadResponse: IFileUploadResult
    {

        private S3UploadResponse() { }

        public static S3UploadResponse Success(
            DateTime utcStartUploading,
            DateTime utcFinishedUploading,
            string key,
            string? filePath = null,
            string? accessUrl = null,
            string? eTag = null,
            MetadataCollection? metadataCollection = null,
            string msg = "Upload finished successfully")
            => new()
            {
                UploadStartedInUtc = utcStartUploading,
                Message = msg,
                ETag = eTag,
                Key = key,
                FilePath = filePath,
                IsSuccess = true,
                AccessUrl = accessUrl,
                MetadataCollection = metadataCollection,
                UploadFinishedInUtc = utcFinishedUploading
            };

        public static S3UploadResponse Failure(FileStorageException exception, DateTime? utcStart = null)
            => new S3UploadResponse()
            {
                Message = exception.Message,
                IsSuccess = false,
                UploadStartedInUtc = utcStart ?? DateTime.UtcNow
            };

        public DateTime UploadStartedInUtc { get; init; }
        public DateTime? UploadFinishedInUtc { get; init; }
        public bool IsSuccess { get; init; }
        public string Message { get; init; } = string.Empty;
        public string? FilePath { get; init; }
        public string? AccessUrl { get; init; }

        // add more s3 specific prop here

        public string? ETag { get; init; }
        public MetadataCollection? MetadataCollection { get; init; }
        public string? Key { get; init; }


    }
}
