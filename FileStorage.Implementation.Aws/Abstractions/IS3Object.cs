

using Amazon.S3;

namespace FileStorage.Implementation.Aws.Abstractions
{
    public interface IS3Object
    {
        public S3CannedACL CannedAcl { get;  }
        public string Key { get;  }
    }
}
