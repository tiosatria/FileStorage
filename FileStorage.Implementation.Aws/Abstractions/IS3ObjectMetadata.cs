

using Amazon.S3.Model;
using FileStorage.Abstraction.Contracts;

namespace FileStorage.Implementation.Aws.Abstractions
{
    public interface IS3ObjectMetadata : IStorageFileMetadata, IS3Object
    {
        public string? ETag { get; }
        public MetadataCollection? MetadataCollection { get; }
    }
}
