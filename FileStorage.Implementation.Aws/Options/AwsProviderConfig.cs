using System.Reflection;
using Microsoft.Extensions.Options;

namespace FileStorage.Implementation.Aws.Options;

// todo finalize
public class AwsProviderConfig
{
    /// <summary>
    /// Bucket Name
    /// </summary>
    /// <remarks>Used for creating S3Client</remarks>
    public string BucketName { get; set; } = string.Empty;
    /// <summary>
    /// Service Url
    /// </summary>
    /// <remarks>Used for creating S3Client</remarks>
    public string ServiceUrl { get; set; } = string.Empty;

    /// <summary>
    /// Signature Version
    /// </summary>
    /// <remarks>Used for creating S3Client</remarks>
    public string SignatureVersion { get; set; } = string.Empty;

    /// <summary>
    /// used to configure force path style in aws s3 config
    /// </summary>
    /// <value>Default to true</value>
    public bool ForcePathStyle { get; set; } = true;

    /// <summary>
    /// Used for identification when you have multiple storage service
    /// </summary>
    /// <value>Default to "fs_sk_aws"</value>
    public string StorageKey { get; set; } = "fs_sk_aws";

    /// <summary>
    /// Used for checking the access key id inside your environment to authentication with AWS
    /// </summary>
    /// <remarks>Do not hard code it here, this is just a reference to check what environment name you have set for this specific instance of access key.</remarks>
    /// <value>Default to "fs_ak_id_aws"</value>
    public string AccessKeyIdEnvironmentName { get; set; } = "fs_ak_id_aws";

    /// <summary>
    /// Used for checking the secret access key inside your environment to authentication with AWS
    /// </summary>
    /// <remarks>Do not hard code it here, this is just a reference to check what environment name you have set for this specific instance of access key.</remarks>
    /// <value>Default to "fs_ak_aws"</value>
    public string AccessKeySecretEnvironmentName { get; set; } = "fs_ak_aws";

}

public class AwsProviderConfigDefault<TProviderConfig> : IConfigureOptions<TProviderConfig>
    where TProviderConfig : AwsProviderConfig
{

    private TProviderConfig? _config;

    protected readonly string ConfigName = typeof(TProviderConfig).Name;

    protected virtual string ErrorMsgTemplate(string cfgName) => $"Please set {cfgName} in your {ConfigName} config.";

    protected void ThrowErrorIfNotSet(Func<TProviderConfig, string?> selector)
    {
        var propValue = selector(_config??throw new InvalidOperationException("Attempted to access config before injecting on Configure()"));

        if (string.IsNullOrWhiteSpace(propValue))
        {
            var propName = selector.Method.Name.Replace("get_", "");
            throw new InvalidOperationException($"Missing required config value: '{propName}' in {ConfigName}.");
        }
        
        // extend
    }


    public virtual void Configure(TProviderConfig options)
    {
        _config = options;
        ThrowErrorIfNotSet(o => o.BucketName);
        ThrowErrorIfNotSet(o => o.ServiceUrl);
        ThrowErrorIfNotSet(o => o.StorageKey);
        ThrowErrorIfNotSet(o => o.AccessKeyIdEnvironmentName);
        ThrowErrorIfNotSet(o => o.AccessKeySecretEnvironmentName);
        if (string.IsNullOrEmpty(options.SignatureVersion))
            options.SignatureVersion = "4";
    }


}