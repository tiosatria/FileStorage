
using FileStorage.Implementation.Aws.ServiceProvider.DigitalOcean;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using FileStorage.Abstraction.Contracts;
using FileStorage.Implementation.Aws.ServiceProvider.Wasabi;


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
            Action<DigitalOceanStorageServiceLifetime> serviceBuilder)
        {
            var service = new DigitalOceanStorageServiceLifetime();
            serviceBuilder.Invoke(service);
            return new DigitalOceanStorageServiceLifetime
                .Builder(service)
                .Build(container);
        }

        /// <summary>
        /// Adds and configures a Wasabi storage provider to the specified service collection.
        /// </summary>
        /// <remarks>This method allows the caller to configure the Wasabi storage provider by providing a
        /// custom <see cref="WasabiStorageLifetime"/> configuration through the <paramref name="serviceBuilder"/>
        /// delegate.</remarks>
        /// <param name="container">The <see cref="IServiceCollection"/> to which the Wasabi storage provider will be added.</param>
        /// <param name="serviceBuilder">A delegate that configures the <see cref="WasabiStorageLifetime"/> instance used to define the storage
        /// provider's behavior.</param>
        /// <returns>The <see cref="IServiceCollection"/> with the Wasabi storage provider added.</returns>
        public static IServiceCollection AddWasabiStorageProvider(this IServiceCollection container,
            Action<WasabiStorageLifetime> serviceBuilder)
        {
            var service = new WasabiStorageLifetime();
            serviceBuilder.Invoke(service);
            return new WasabiStorageLifetime.Builder(service).Build(container);
        }

    }
}
