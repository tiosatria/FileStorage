using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using FileStorage.Abstraction.Contracts;
using FileStorage.Abstraction.Events;
using FileStorage.Core.Exceptions;
using FileStorage.Implementation.Aws.Acl;
using FileStorage.Implementation.Aws.Models;

namespace FileStorage.Implementation.Aws
{
    /// <summary>
    /// Abstract Orchestrator class for providing S3Storage Service
    /// <para>Add virtual in method that really need override</para>
    /// <para>Otherwise, use the base, it should suffice</para>
    /// </summary>
    /// <remarks>You will need to implement it on your specific provider use case. e.g digital ocean etc...</remarks>
    /// <param name="client"></param>
    /// <param name="bucketName"></param>
    /// <param name="storageKey"></param>
    public abstract class S3StorageService(
        IAmazonS3 client,
        string bucketName,
        string storageKey)
        : IFileStorageService
    {
        protected IAmazonS3 Client { get; init; } = client;

        public string BucketName { get; init; } = bucketName;

        public string StorageKey { get; protected init; } = storageKey;

        protected abstract Version StorageVersion { get; }

        protected abstract string? MakeAccessUrl(string key);

        // override your convention here
        protected virtual string MakeFilePath(string filePath) => $"{StorageKey}://{filePath}";

        public async Task<IFileUploadResult> UploadAsync(IUploadStorageObject uploadStorageObject,
            EventHandler<StorageTransferProgressArgs>? uploadProgressEvent = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (uploadStorageObject is not S3UploadObject s3Obj)
                    throw new IncompatibleUploadObjectException<S3UploadObject>(uploadStorageObject);

                var key = s3Obj.Key;

                cancellationToken.ThrowIfCancellationRequested();
                using var transferUtility = new TransferUtility(Client);
                var request = new TransferUtilityUploadRequest
                {
                    InputStream = uploadStorageObject.Content,
                    Key = key,
                    BucketName = BucketName,
                    ContentType = uploadStorageObject.ContentType,
                    CannedACL = s3Obj.CannedAcl,
                    AutoCloseStream = false,
                    AutoResetStreamPosition = true
                };

                cancellationToken.ThrowIfCancellationRequested();
                if (uploadProgressEvent != null)
                    request.UploadProgressEvent += (sender, args) =>
                    {
                        uploadProgressEvent.Invoke(sender,
                            new StorageTransferProgressArgs(args.TotalBytes, args.TransferredBytes));
                    };

                cancellationToken.ThrowIfCancellationRequested();

                var utcStartUpload = DateTime.UtcNow;
                await transferUtility.UploadAsync(request, cancellationToken);
                var utcFinished = DateTime.UtcNow;

                return S3UploadResponse.Success(utcStartUpload, utcFinished, key, MakeFilePath(key), MakeAccessUrl(key), request.TagSet.ToString(), request.Metadata);

            }
            catch (FileStorageException e)
            {
                return S3UploadResponse.Failure(e);
            }
            // handle unhandled exception
            catch
            {
                throw;
            }
        }

        public async Task<IStorageObject> DownloadAsync(string path, EventHandler<StorageTransferProgressArgs>? onDownloadProgressChanged = null,
            CancellationToken cancellationToken = default)
        {
            var acl = await new S3AclResolver(Client, BucketName, path, cancellationToken).ResolveAclAsync();

            cancellationToken.ThrowIfCancellationRequested();

            var res = await Client.GetObjectAsync(BucketName, path, cancellationToken);

            return S3StorageObject.FromResponse(res, acl.ToCannedAcl());
        }

        public async Task<bool> DeleteAsync(string path, CancellationToken cancellationToken = default)
        {
            try
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    Key = path,
                    BucketName = BucketName
                };

                var response = await Client.DeleteObjectAsync(deleteObjectRequest, cancellationToken);
                return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsExistAsync(string path, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new GetObjectMetadataRequest
                {
                    Key = path,
                    BucketName = BucketName
                };
                await Client.GetObjectMetadataAsync(request, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IStorageFileMetadata> GetFileMetadataAsync(string path, CancellationToken cancellationToken = default)
        {
            var request = new GetObjectMetadataRequest
            {
                Key = path,
                BucketName = BucketName
            };

            var response = await Client.GetObjectMetadataAsync(request, cancellationToken);
            var acl = await new S3AclResolver(Client, BucketName, path, cancellationToken).ResolveAclAsync();
            return S3ObjectMetadata.FromObjectMetadataResult(path, response, acl.ToCannedAcl());
        }

        /// <summary>
        /// Please cache this result as it is expensive to call
        /// </summary>
        /// <returns>Storage information</returns>
        public abstract Task<IStorageInfo> GetStorageInfoAsync(CancellationToken cancellationToken = default);

    }
}
