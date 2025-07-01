
namespace FileStorage.Abstraction.Events
{
    public class StorageTransferProgressArgs : EventArgs
    {

        public StorageTransferProgressArgs(long totalBytes, long transferredBytes, double percentDone)
        {
            TotalBytes = totalBytes;
            TransferredBytes = transferredBytes;
            PercentDone = percentDone;
        }

        public StorageTransferProgressArgs(long totalBytes, long transferredBytes)
        {
            TotalBytes = totalBytes;
            TransferredBytes = transferredBytes;
            PercentDone = totalBytes > 0
                ? (double)transferredBytes / totalBytes * 100
                : 0;
        }

        public long TransferredBytes { get; init; } 
        public long TotalBytes { get; init; }
        public double PercentDone { get; init; }
    }
}
