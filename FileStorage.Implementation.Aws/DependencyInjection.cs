
using FileStorage.Implementation.Aws.ServiceProvider.DigitalOcean;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using FileStorage.Abstraction.Contracts;


namespace FileStorage.Implementation.Aws
{
    public static class DependencyInjection
    {

        /// <summary>
        /// Registers the <see cref="DigitalOceanStorageService"/> implementation as an <see cref="IFileStorageService"/>
        /// in the dependency injection container. Supports both standard and keyed singleton registration.
        /// </summary>
        /// <param name="container">The service collection to register the storage provider into.</param>
        /// <param name="serviceBuilder">
        /// A configuration delegate to define the service’s DI behavior (e.g., keyed singleton, concrete type).
        /// </param>
        /// <remarks>
        /// To work correctly, ensure <see cref="IOptions{DigitalOceanProviderConfig}"/> is registered and configured.
        /// The provider reads sensitive access credentials from environment variables defined in the config.
        /// </remarks>
        /// <example>
        /// <code>
        /// services.Configure&lt;DigitalOceanProviderConfig&gt;(config =&gt;
        /// {
        ///     config.BucketName = "my-bucket";
        ///     config.CdnUrl = "https://cdn.mybucket.digitaloceanspaces.com";
        ///     config.AccessKeyIdEnvironmentName = "DO_SPACES_KEY";
        ///     config.AccessKeySecretEnvironmentName = "DO_SPACES_SECRET";
        ///     config.ServiceUrl = "https://nyc3.digitaloceanspaces.com";
        /// });
        ///
        /// services.AddAwsDigitalOceanStorageProvider(opt =&gt; opt.AsSingleton());
        /// </code>
        /// </example>
        /// <code>
        /// </code>
        /// <returns>The updated <see cref="IServiceCollection"/> with the storage provider registered.</returns>

        public static IServiceCollection AddAwsDigitalOceanStorageProvider(this IServiceCollection container,
            Action<DigitalOceanService> serviceBuilder)
        {
            var service = new DigitalOceanService();
            serviceBuilder.Invoke(service);
            return new DigitalOceanService
                .Builder(service)
                .Build(container);
        }
    }
}
