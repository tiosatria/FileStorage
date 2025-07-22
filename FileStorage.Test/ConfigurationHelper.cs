

using Amazon.S3;
using FileStorage.Implementation.Aws.Options;
using FileStorage.Implementation.Aws.ServiceProvider.Wasabi;

namespace FileStorage.Test
{
    public static class ConfigurationHelper
    {
        public static AmazonS3Client GetAmazonS3Client(AwsProviderConfig cfg)
        {
            var config = new AmazonS3Config()
            {
                ForcePathStyle = cfg.ForcePathStyle,
                ServiceURL = cfg.ServiceUrl
            };
            // for debug only
            var ak = Environment.GetEnvironmentVariable(cfg.AccessKeyIdEnvironmentName);
            var sk = Environment.GetEnvironmentVariable(cfg.AccessKeySecretEnvironmentName);
            return new AmazonS3Client(ak,sk, config);

        }

        public static WasabiProviderConfig GetWasabiTestConfiguration()
        {
           
            return new WasabiProviderConfig()
            {
                BucketName = "your bucket name",
                StorageKey = nameof(WasabiStorageService),
                ForcePathStyle = true,
                AccessKeySecretEnvironmentName = "wasabi-secret-key",
                SignatureVersion = "4",
                ServiceUrl = "https://s3.ap-southeast-1.wasabisys.com",
                AccessKeyIdEnvironmentName = "wasabi-access-key",
                IsTrialAccount = true
            };
        }
    }
}
