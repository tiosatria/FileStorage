
using Amazon.S3.Model;
using FileStorage.Abstraction.Contracts;

namespace FileStorage.Implementation.Aws.Abstractions
{
    public interface IS3StorageObject : IStorageObject, IS3Object, IS3ObjectMetadata
    {
       
    }
}
