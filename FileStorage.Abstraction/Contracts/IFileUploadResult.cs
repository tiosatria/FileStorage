namespace FileStorage.Abstraction.Contracts
{
    public interface IFileUploadResult
    {
        public DateTime UploadStartedInUtc { get; }
        public DateTime? UploadFinishedInUtc { get; }
        public bool IsSuccess { get; }
        public string Message { get; }
        public string FilePath { get; set; }
        public string AccessUrl { get; }
    }
}
