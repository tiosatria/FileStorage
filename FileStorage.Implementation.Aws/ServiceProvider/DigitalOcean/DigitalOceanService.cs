
using Amazon.S3;
using FileStorage.Core;
using FileStorage.Implementation.Aws.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FileStorage.Implementation.Aws.ServiceProvider.DigitalOcean
{
    public class DigitalOceanService() : StorageServiceLifetime<DigitalOceanStorageService>
    {
        protected sealed override DigitalOceanStorageService BuildUnderlyingService(IServiceProvider sp)
        {
            var opt = sp.GetRequiredService<IOptions<DigitalOceanProviderConfig>>().Value;

            var config = new AmazonS3Config()
            {
                ForcePathStyle = opt.ForcePathStyle,
                ServiceURL = opt.ServiceUrl,
            };

            var client = new AmazonS3Client(Environment.GetEnvironmentVariable(opt.AccessKeyIdEnvironmentName),
                Environment.GetEnvironmentVariable(opt.AccessKeySecretEnvironmentName), config);
            
            return new DigitalOceanStorageService(client, opt.BucketName, opt.StorageKey, opt.CdnUrl);
        }
    }
}
