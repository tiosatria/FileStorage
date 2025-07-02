
using Amazon.S3;
using Amazon.S3.Model;
using FileStorage.Core.Models;
using FileStorage.Implementation.Aws.Abstractions;
using FileStorage.Implementation.Aws.Acl.Extension;

namespace FileStorage.Implementation.Aws.Models
{
    public class S3ObjectMetadata : GenericObjectMetadata,IS3ObjectMetadata
    {
        public S3ObjectMetadata(IS3ObjectMetadata storageObj) : base(storageObj)
        {
            ETag = storageObj.ETag;
            MetadataCollection=storageObj.MetadataCollection;
            Key = storageObj.Key;
            CannedAcl = storageObj.CannedAcl;
        }

        public S3ObjectMetadata(string key, S3CannedACL cannedAcl, long contentLengthBytes, 
            string? eTag = null,
            MetadataCollection? metadataCollection = null,
            DateTime? utcCreated = null,DateTime? utcModified = null, string? contentType = null):base(key,contentLengthBytes, utcCreated, utcModified, cannedAcl.ToFileVisibilityEnum(), contentType)
        {
            Key=key;
            ETag = eTag;
            CannedAcl=cannedAcl;
            MetadataCollection = metadataCollection;
        }

        public static S3ObjectMetadata FromObjectMetadataResult(
            string key, GetObjectMetadataResponse metadata, S3CannedACL cannedAcl)
        {
            return new S3ObjectMetadata(key, cannedAcl, metadata.ContentLength, metadata.ETag, metadata.Metadata,
                metadata.LastModified, metadata.LastModified, metadata.Headers.ContentType);
        }

        public S3CannedACL CannedAcl { get; init; }
        public string Key { get; init; }
        public string? ETag { get; init; }
        public MetadataCollection? MetadataCollection { get; init; }
    }
}
