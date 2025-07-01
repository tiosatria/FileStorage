namespace FileStorage.Abstraction.Contracts
{
    public interface IUploadStorageObject : IStorageObject 
    {
        public string DestinationPath { get; }
    }
}
