

using Amazon.S3;
using Amazon.S3.Model;
using FileStorage.Abstraction.Contracts;
using FileStorage.Core.Models;
using FileStorage.Implementation.Aws.Abstractions;
using FileStorage.Implementation.Aws.Acl.Extension;

namespace FileStorage.Implementation.Aws.Models
{
    public class S3StorageObject : GenericStorageObject, IS3StorageObject
    {

        public static S3StorageObject FromResponse(GetObjectResponse resp, S3CannedACL acl) =>
            new(resp, acl);

        public S3StorageObject(Stream stream, string key, S3CannedACL acl, string? contentType = null, string? eTag = null, MetadataCollection? metadata = null) : base(stream, key, contentType, acl.ToFileVisibilityEnum())
        {
            Key = key;
            ETag = eTag;
            MetadataCollection = metadata;
            CannedAcl= acl;
        }

        public S3StorageObject(GetObjectResponse getObj, S3CannedACL acl) : base(getObj.ResponseStream, getObj.Key, visibility: acl.ToFileVisibilityEnum())
        {
            CannedAcl = acl;
            Key = getObj.Key;
            ETag = getObj.ETag;
            MetadataCollection = getObj.Metadata;
        }

        public S3CannedACL CannedAcl { get; init; }

        public string Key { get; }
        public string? ETag { get; init; }
        public MetadataCollection? MetadataCollection { get; init; }

    }
}
