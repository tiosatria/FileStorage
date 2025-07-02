
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;

namespace FileStorage.Implementation.Aws.Acl
{
    public class S3AclResolver(IAmazonS3 client, string bucketName, string key, CancellationToken cancellationToken = default)
    {
        public async Task<ResolvedS3Acl> ResolveAclAsync()
        {
            var aclReq = new GetObjectAclRequest()
            {
                BucketName = bucketName,
                Key = key,
            };
            cancellationToken.ThrowIfCancellationRequested();
            var res = await client.GetObjectAclAsync(aclReq, cancellationToken);
            return new ResolvedS3Acl(res);
        }

    }
}
