using Amazon.S3;
using Amazon.S3.Model;

namespace FileStorage.Implementation.Aws.Acl;

public class ResolvedS3Acl(GetObjectAclResponse resp)
{

    public S3CannedACL ToCannedAcl() => IsPublicRead() ? S3CannedACL.PublicRead : S3CannedACL.Private;

    public bool IsPublicRead()
        => AclResponse.Grants.Any(grant =>
            grant.Permission == S3Permission.READ && grant.Grantee.URI.Equals(S3GranteeUris.AllUsers));

    public GetObjectAclResponse AclResponse { get; init; } = resp;

}