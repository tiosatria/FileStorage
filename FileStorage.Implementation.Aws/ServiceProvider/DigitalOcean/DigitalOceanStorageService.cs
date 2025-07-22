

using Amazon.S3;
using FileStorage.Abstraction.Contracts;
using FileStorage.Abstraction.Models;
using FileStorage.Implementation.Aws.ServiceProvider.Wasabi;

namespace FileStorage.Implementation.Aws.ServiceProvider.DigitalOcean
{
    public class DigitalOceanStorageService(
        IAmazonS3 client, 
        string bucketName, 
        string storageKey, 
        string? cdnUrl = null) : S3StorageService(client, bucketName, storageKey)
    {
        static DigitalOceanStorageService()
        {
            Ver = new Version(0, 1);
        }

        public string? CdnUrl = cdnUrl;

        static readonly Version Ver;

        protected override Version StorageVersion => Ver;

        public override string MakeAccessUrl(string key)
            => CdnUrl != null ? $"{Path.Combine(CdnUrl, key)}" : Path.Combine(Client.Config.ServiceURL, key);

        // 500gigs limit on DO storage
        private const long MaxStorageSize = (long)500 * 1024 * 1024 * 1024;

        // todo : finish this (when have DO creds)
        /// <summary>
        /// Please cache this result as it is expensive to call
        /// </summary>
        /// <returns>Storage information</returns>
        public override Task<IStorageInfo> GetStorageInfoAsync(CancellationToken cancellationToken = default)
        {
            // todo: implement total use in bytes later. preferably in base class
            return Task.FromResult<IStorageInfo>(new StorageInfo(StorageKey, nameof(WasabiStorageService), StorageVersion.ToString(), MaxStorageSize, 0));
        }

    }
}
