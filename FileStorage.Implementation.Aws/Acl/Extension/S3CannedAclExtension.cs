
using Amazon.S3;
using FileStorage.Abstraction.Contracts;

namespace FileStorage.Implementation.Aws.Acl.Extension
{
    internal static class S3CannedAclExtension
    {
        public static FileVisibilityEnum ToFileVisibilityEnum(this S3CannedACL cannedAcl)
            => cannedAcl.Value switch
            {
                "private" => FileVisibilityEnum.Private,
                _ => FileVisibilityEnum.Public
            };
    }
}
