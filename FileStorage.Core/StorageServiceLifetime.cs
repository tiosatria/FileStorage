using FileStorage.Abstraction.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace FileStorage.Core;

/// <summary>
/// Builder base class for registering a file storage implementation with the dependency injection container.
/// Supports flexible lifetimes and resolution strategies (interface vs concrete type, keyed vs default).
/// </summary>
/// <typeparam name="TUnderlyingService">
/// The file storage service type to register. Must implement <see cref="IFileStorageService"/>.
/// </typeparam>
public abstract class StorageServiceLifetime<TUnderlyingService>
    where TUnderlyingService : class, IFileStorageService
{
    /// <summary>
    /// Describes how the service should be registered in the container.
    /// </summary>
    public enum LifetimeDescriptorEnum
    {
        /// <summary>Default singleton registration without keying.</summary>
        Singleton,

        /// <summary>Singleton registered using a resolution key (i.e. keyed DI).</summary>
        KeyedSingleton
    }

    /// <summary>
    /// Optional key used for keyed registration, if applicable.
    /// </summary>
    protected string? Key { get; private set; }

    /// <summary>
    /// Chosen lifetime descriptor for the service registration.
    /// </summary>
    protected LifetimeDescriptorEnum LifetimeDescriptor { get; private set; } = LifetimeDescriptorEnum.Singleton;

    /// <summary>
    /// Whether the service should be registered as its concrete type instead of the IFileStorageService interface.
    /// </summary>
    private bool _registerAsConcreteType;

    /// <summary>
    /// Register as a keyed singleton for <see cref="IFileStorageService"/>.
    /// </summary>
    public StorageServiceLifetime<TUnderlyingService> AsKeyedSingleton(string singletonKey)
    {
        Key = singletonKey;
        _registerAsConcreteType = false;
        LifetimeDescriptor = LifetimeDescriptorEnum.KeyedSingleton;
        return this;
    }

    /// <summary>
    /// Register as a default singleton for <see cref="IFileStorageService"/>.
    /// </summary>
    public StorageServiceLifetime<TUnderlyingService> AsSingleton()
    {
        LifetimeDescriptor = LifetimeDescriptorEnum.Singleton;
        _registerAsConcreteType = false;
        return this;
    }

    /// <summary>
    /// Register as a singleton using the concrete type instead of the interface.
    /// </summary>
    public StorageServiceLifetime<TUnderlyingService> AsSingletonOfConcrete()
    {
        _registerAsConcreteType = true;
        LifetimeDescriptor = LifetimeDescriptorEnum.Singleton;
        return this;
    }

    /// <summary>
    /// Register as a keyed singleton using the concrete type.
    /// </summary>
    public StorageServiceLifetime<TUnderlyingService> AsKeyedSingletonOfConcrete(string singletonKey)
    {
        Key = singletonKey;
        _registerAsConcreteType = true;
        LifetimeDescriptor = LifetimeDescriptorEnum.KeyedSingleton;
        return this;
    }

    /// <summary>
    /// Resolves and builds the actual implementation of the storage service.
    /// </summary>
    /// <param name="sp">The service provider context.</param>
    protected abstract TUnderlyingService BuildUnderlyingService(IServiceProvider sp);

    public sealed class Builder(StorageServiceLifetime<TUnderlyingService> lifetimeConfig)
    {
        private IServiceCollection RegisterSingleton(IServiceCollection container)
        {
            return lifetimeConfig._registerAsConcreteType
                ? container.AddSingleton(lifetimeConfig.BuildUnderlyingService)
                : container.AddSingleton<IFileStorageService>(lifetimeConfig.BuildUnderlyingService);
        }

        private IServiceCollection RegisterKeyed(IServiceCollection container)
        {
            if (string.IsNullOrWhiteSpace(lifetimeConfig.Key))
                throw new InvalidOperationException("A valid key must be provided for keyed singleton registration.");

            return lifetimeConfig._registerAsConcreteType
                ? container.AddKeyedSingleton(lifetimeConfig.Key, (sp, _) => lifetimeConfig.BuildUnderlyingService(sp))
                : container.AddKeyedSingleton<IFileStorageService, TUnderlyingService>(
                    lifetimeConfig.Key, (sp, _) => lifetimeConfig.BuildUnderlyingService(sp));
        }


        /// <summary>
        /// Executes the service registration into the provided DI container.
        /// </summary>
        /// <param name="container">The service collection to register into.</param>
        /// <returns>The updated service collection.</returns>
        public IServiceCollection BuildService(IServiceCollection container)
        {
            return lifetimeConfig.LifetimeDescriptor switch
            {
                LifetimeDescriptorEnum.KeyedSingleton => RegisterKeyed(container),
                LifetimeDescriptorEnum.Singleton => RegisterSingleton(container),
                _ => throw new NotSupportedException($"Unsupported lifetime: {lifetimeConfig.LifetimeDescriptor}")
            };
        }

        /// <summary>
        /// Shortcut alias for <see cref="BuildService"/>.
        /// </summary>
        public IServiceCollection Build(IServiceCollection services) => BuildService(services);
    }


}
