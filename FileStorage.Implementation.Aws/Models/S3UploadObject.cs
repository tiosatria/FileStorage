

using Amazon.S3;
using FileStorage.Abstraction.Contracts;
using FileStorage.Core.Models;
using FileStorage.Implementation.Aws.Abstractions;

namespace FileStorage.Implementation.Aws.Models
{
    public class S3UploadObject : GenericUploadStorageObject, IS3Object
    {

        public static S3CannedACL AdaptVisibilityToAcl(FileVisibilityEnum visibility) => visibility switch
        {
            FileVisibilityEnum.Private => S3CannedACL.Private,
            FileVisibilityEnum.Public => S3CannedACL.PublicRead,
            _ => S3CannedACL.Private
        };

        public S3UploadObject(Stream content, string key, string? contentType = null, FileVisibilityEnum visibility = FileVisibilityEnum.Private) : base(content, key, contentType, visibility)
        {
            CannedAcl = AdaptVisibilityToAcl(visibility);
        }

        public S3UploadObject(Stream content, string pathPrefix, string fileName, string? contentType = null, FileVisibilityEnum visibility = FileVisibilityEnum.Public) : base(content, Path.Combine(pathPrefix, fileName), contentType, visibility)
        {
            CannedAcl = AdaptVisibilityToAcl(visibility);
        }

        public S3CannedACL CannedAcl { get; init; }
        public string Key => DestinationPath;

    }
}
