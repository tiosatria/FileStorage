
using FileStorage.Abstraction.Contracts;
using HeyRed.Mime;

namespace FileStorage.Core.Models
{
    public abstract class GenericStorageObject : IStorageObject, IDisposable, IAsyncDisposable
    {

        /// <summary>
        /// Local storage constructor
        /// Init new generic storage object from local path
        /// </summary>
        /// <remarks>handle exception using your own method, not recommend to use this constructor</remarks>
        /// <remarks>Use this constructor only if you're sure the file is from the local path</remarks>
        /// <param name="fullPath"></param>
        /// <param name="visibility"></param>
        public GenericStorageObject(
            string fullPath, 
            FileVisibilityEnum visibility = FileVisibilityEnum.Private)
        {
            FileName = Path.GetFileName(fullPath);
            FilePath = fullPath;
            Visibility = visibility;
            ContentType = Utilities.GuessContentType(fullPath);
            FileExtension = Path.GetExtension(FileName);
            Content = File.OpenRead(fullPath);
            LastModifiedUtc = File.GetLastWriteTimeUtc(fullPath);
            CreatedAtUtc = File.GetCreationTimeUtc(fullPath);
        }

        /// <summary>
        /// Local storage constructor
        /// </summary>
        /// <remarks>Use this constructor only if you're sure the file is from the local path</remarks>
        /// <param name="stream"></param>
        /// <param name="visibility"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public GenericStorageObject(Stream stream, FileVisibilityEnum visibility = FileVisibilityEnum.Private)
        {
            Content = stream;
            ContentType= Utilities.GuessContentType(stream);
            FileExtension = Utilities.GuessExtension(stream);
            if (stream is not FileStream fs)
                throw new InvalidOperationException("using solely stream constructor while not using FileStream");
            FileName = Path.GetFileName(fs.Name);
            FilePath = fs.Name;
            Visibility = visibility;
            // reset to avoid funny business
            if(stream.CanSeek)
                stream.Position = 0;
            LastModifiedUtc = File.GetLastWriteTime(fs.Name);
            CreatedAtUtc = File.GetCreationTimeUtc(fs.Name);
        }

        /// <summary>
        /// Use this for third party or file from outside of local
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fullPath"></param>
        /// <param name="contentType"></param>
        /// <param name="createdUtc"></param>
        /// <param name="lastModifiedUtc"></param>
        /// <param name="visibility"></param>
        public GenericStorageObject(
            Stream stream, 
            string fullPath, 
            string? contentType = null, 
            DateTime? createdUtc = null,
            DateTime? lastModifiedUtc = null,
            FileVisibilityEnum visibility = FileVisibilityEnum.Private)
        {
            FileName = Path.GetFileName(fullPath);
            FilePath= fullPath;
            Visibility = visibility;
            Content = stream;
            ContentType = contentType?? Utilities.GuessContentType(fullPath);
            FileExtension = Path.GetExtension(fullPath);
            CreatedAtUtc = createdUtc;
            LastModifiedUtc = lastModifiedUtc;
        }

        public string FilePath { get; init; }
        public string FileName { get; init; }
        public FileVisibilityEnum Visibility { get; init; }
        public string ContentType { get; init; }
        public long ContentLengthBytesBytes => Content.Length;
        public string? FileExtension { get;  init; }

        public DateTime? LastModifiedUtc { get; init; }
        public DateTime? CreatedAtUtc { get; init; }

        public Stream Content { get; init; }

        public void Dispose()
        {
            Content.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await Content.DisposeAsync();
        }
    }
}
