
using Amazon.S3;
using FileStorage.Abstraction.Contracts;
using FileStorage.Abstraction.Models;

namespace FileStorage.Implementation.Aws.ServiceProvider.Wasabi
{
    public sealed class WasabiStorageService(IAmazonS3 client, string bucketName, string storageKey, bool isTrialUser) : S3StorageService(client, bucketName, storageKey)
    {

        // public for checking in external module
        public readonly bool IsTrialUser = isTrialUser;

        protected override Version StorageVersion => new Version(0, 1);

        public override string MakeAccessUrl(string key)
        {
            // trial user don't have public access
            // gotta pre-sign it first, but also have to keep track of the pre-signed, because we don't want to keep pre-signing the file. right?
            // for now, we will not handle that responsibility here, for best practice, you should have your own mechanism to serve the file url to user. maybe via IUrlResourceGenerator
            // alas, we'll just sign it here.
            return IsTrialUser ? GetPreSignedUrl(key, TimeSpan.FromHours(2)) : Path.Combine(Client.Config.ServiceURL, key);
        }

        // hard coded because wasabi does not have hard limit on the storage
        private const long MaxStorageSize = (long)1024 * 1024 * 1024 * 1024;

        public override Task<IStorageInfo> GetStorageInfoAsync(CancellationToken cancellationToken = default)
        {
            // todo: implement total use in bytes later. preferably in base class
            return Task.FromResult<IStorageInfo>(new StorageInfo(StorageKey, nameof(WasabiStorageService), StorageVersion.ToString(), MaxStorageSize, 0));
        }


    }
}
