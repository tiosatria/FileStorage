

using Amazon.S3;
using FileStorage.Abstraction.Contracts;

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

        /// <summary>
        /// Please cache this result as it is expensive to call
        /// </summary>
        /// <returns>Storage information</returns>
        public override Task<IStorageInfo> GetStorageInfoAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

    }
}
