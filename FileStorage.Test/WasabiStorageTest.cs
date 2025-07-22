using FileStorage.Abstraction.Contracts;
using FileStorage.Implementation.Aws.ServiceProvider.Wasabi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using FileStorage.Implementation.Aws.Options;

namespace FileStorage.Test
{
    public class WasabiStorageServiceTest() : AbstractAwsStorageServiceTest<WasabiStorageService>(CreateWasabiStorage())
    {
        private static WasabiStorageService CreateWasabiStorage()
        {
            var config = ConfigurationHelper.GetWasabiTestConfiguration();

            var awsClient = ConfigurationHelper.GetAmazonS3Client(config);

            return new WasabiStorageService(awsClient, config.BucketName, config.StorageKey, config.IsTrialAccount);
        }



    }
}
