using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using FileStorage.Core;
using FileStorage.Implementation.Aws.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FileStorage.Implementation.Aws.ServiceProvider.Wasabi
{
    public class WasabiStorageLifetime : StorageServiceLifetime<WasabiStorageService>
    {
        protected override WasabiStorageService BuildUnderlyingService(IServiceProvider sp)
        {
            var opt = sp.GetRequiredService<IOptions<WasabiProviderConfig>>().Value;

            var config = new AmazonS3Config()
            {
                ForcePathStyle = opt.ForcePathStyle,
                ServiceURL = opt.ServiceUrl
            };

            var client = new AmazonS3Client(Environment.GetEnvironmentVariable(opt.AccessKeyIdEnvironmentName),
                Environment.GetEnvironmentVariable(opt.AccessKeySecretEnvironmentName), config);

            return new WasabiStorageService(client, opt.BucketName, opt.StorageKey, opt.IsTrialAccount);
        }
    }
}
