

using FileStorage.Abstraction.Contracts;
using FileStorage.Implementation.Aws;
using FileStorage.Implementation.Aws.Models;

namespace FileStorage.Test
{
    public abstract class AbstractAwsStorageServiceTest<TS3StorageService>(TS3StorageService storageService) : AbstractStorageServiceTest<TS3StorageService>(storageService) where  TS3StorageService : S3StorageService
    {
        protected override IUploadStorageObject PrepareUploadObject(Stream content, string destinationPath)
        {
            return new S3UploadObject(content, destinationPath);
        }
    }
}
