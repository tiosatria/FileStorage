
using FileStorage.Abstraction.Contracts;
using HeyRed.Mime;

namespace FileStorage.Core.Models
{
    public abstract class GenericObjectMetadata : IStorageFileMetadata
    {

        protected GenericObjectMetadata(IStorageFileMetadata storageObj)
        {
            FileName = storageObj.FileName;
            FileExtension = storageObj.FileExtension;
            FilePath = storageObj.FilePath;
            Visibility = storageObj.Visibility;
            ContentType = storageObj.ContentType;
            ContentLengthBytes = storageObj.ContentLengthBytes;
            FileExtension = storageObj.FileExtension;
            CreatedAtUtc = storageObj.CreatedAtUtc;
            LastModifiedUtc = storageObj.LastModifiedUtc;
        }

        /// <summary>
        /// Local file constructor
        /// </summary>
        ///  <param name="fullPath"></param>
        /// <param name="contentType"></param>
        /// <param name="visibility"></param>
        protected GenericObjectMetadata(string fullPath,string? contentType = null, FileVisibilityEnum visibility = FileVisibilityEnum.Private)
        {
            FilePath=fullPath;
            FileName = Path.GetFileName(fullPath);
            Visibility = visibility;
            ContentType = contentType ?? Utilities.GuessContentType(fullPath);
            var info = new FileInfo(fullPath);
            ContentLengthBytes = info.Length;
            FileExtension = info.Extension;
            LastModifiedUtc = info.LastWriteTimeUtc;
            CreatedAtUtc = info.CreationTimeUtc;
        }

        protected GenericObjectMetadata(string fullPath, long contentLengthBytes, DateTime? utcCreated, DateTime? utcModified, FileVisibilityEnum visibility = FileVisibilityEnum.Private, string? contentType = null)
        {
            FilePath = fullPath;
            (FileName, FileExtension,ContentType) = Utilities.GetBaseFileInfoFromPath(fullPath);
            ContentLengthBytes=contentLengthBytes;
            CreatedAtUtc=utcCreated;
            LastModifiedUtc=utcModified;
            Visibility = visibility;
        }

        public string FilePath { get; init; }
        public string FileName { get; init; }
        public FileVisibilityEnum Visibility { get; init; }
        public string ContentType { get; init; }
        public long ContentLengthBytes { get; init; }
        public string? FileExtension { get; init; }
        public DateTime? LastModifiedUtc { get; init; }
        public DateTime? CreatedAtUtc { get; init; }

    }
}
