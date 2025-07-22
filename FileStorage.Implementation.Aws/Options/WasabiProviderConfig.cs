

namespace FileStorage.Implementation.Aws.Options
{
    /// <summary>
    /// reference : https://docs.wasabi.com/docs/how-do-i-use-aws-sdk-for-net-with-wasabi
    /// </summary>
    public sealed class WasabiProviderConfig : AwsProviderConfig
    {
        // more wasabi-related config here
        public bool IsTrialAccount { get; set; } 
    }
}
