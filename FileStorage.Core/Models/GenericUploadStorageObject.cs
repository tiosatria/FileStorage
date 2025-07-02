
using FileStorage.Abstraction.Contracts;
using HeyRed.Mime;

namespace FileStorage.Core.Models
{
    public abstract class GenericUploadStorageObject : IUploadStorageObject
    {
        protected GenericUploadStorageObject(Stream content, string destinationPath, string? contentType = null, FileVisibilityEnum visibility = FileVisibilityEnum.Private)
        {
            Content = content;
            DestinationPath = destinationPath;
            ContentType = contentType ?? MimeGuesser.GuessMimeType(content);
            ContentLengthBytes = content.Length;
            Visibility = visibility;
            content.Position = 0;
            FileExtension = Path.GetExtension(destinationPath);
        }

        public string FileExtension { get; init; }

        public Stream Content { get; }
        public string ContentType { get; }
        public long ContentLengthBytes { get; }
        public FileVisibilityEnum Visibility { get; }
        public string DestinationPath { get; }

    }
}
