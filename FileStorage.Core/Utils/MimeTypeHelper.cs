
namespace FileStorage.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class MimeTypeHelper
    {
        private static readonly Dictionary<string, string> MimeTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        // Images
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".png", "image/png" },
        { ".gif", "image/gif" },
        { ".bmp", "image/bmp" },
        { ".webp", "image/webp" },
        { ".svg", "image/svg+xml" },
        { ".ico", "image/x-icon" },
        { ".tiff", "image/tiff" },
        { ".tif", "image/tiff" },

        // Documents
        { ".pdf", "application/pdf" },
        { ".doc", "application/msword" },
        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { ".xls", "application/vnd.ms-excel" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".ppt", "application/vnd.ms-powerpoint" },
        { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
        { ".rtf", "application/rtf" },

        // Text
        { ".txt", "text/plain" },
        { ".html", "text/html" },
        { ".htm", "text/html" },
        { ".css", "text/css" },
        { ".js", "application/javascript" },
        { ".json", "application/json" },
        { ".xml", "application/xml" },
        { ".csv", "text/csv" },

        // Archives
        { ".zip", "application/zip" },
        { ".rar", "application/x-rar-compressed" },
        { ".7z", "application/x-7z-compressed" },
        { ".tar", "application/x-tar" },
        { ".gz", "application/gzip" },

        // Audio
        { ".mp3", "audio/mpeg" },
        { ".wav", "audio/wav" },
        { ".ogg", "audio/ogg" },
        { ".flac", "audio/flac" },
        { ".aac", "audio/aac" },

        // Video
        { ".mp4", "video/mp4" },
        { ".avi", "video/x-msvideo" },
        { ".mov", "video/quicktime" },
        { ".wmv", "video/x-ms-wmv" },
        { ".flv", "video/x-flv" },
        { ".webm", "video/webm" },

        // Other
        { ".exe", "application/octet-stream" },
        { ".bin", "application/octet-stream" },
        { ".dll", "application/octet-stream" }
    };

        /// <summary>
        /// Gets the MIME content type for a file extension
        /// </summary>
        /// <param name="extension">File extension (with or without leading dot)</param>
        /// <returns>MIME content type or "application/octet-stream" as default</returns>
        public static string GetContentType(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return "application/octet-stream";

            // Ensure extension starts with a dot
            if (!extension.StartsWith("."))
                extension = "." + extension;

            return MimeTypes.GetValueOrDefault(extension, "application/octet-stream");
        }

        /// <summary>
        /// Gets the MIME content type from a filename
        /// </summary>
        /// <param name="filename">Full filename</param>
        /// <returns>MIME content type or "application/octet-stream" as default</returns>
        public static string GetContentTypeFromFilename(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return "application/octet-stream";

            var extension = Path.GetExtension(filename);
            return GetContentType(extension);
        }

        /// <summary>
        /// Checks if the extension is supported
        /// </summary>
        /// <param name="extension">File extension</param>
        /// <returns>True if supported, false otherwise</returns>
        public static bool IsSupported(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return false;

            if (!extension.StartsWith("."))
                extension = "." + extension;

            return MimeTypes.ContainsKey(extension);
        }
    }

    // Usage examples:
    public class Usage
    {
        public static void Examples()
        {
            // From extension
            string contentType1 = MimeTypeHelper.GetContentType(".jpg");     // "image/jpeg"
            string contentType2 = MimeTypeHelper.GetContentType("png");      // "image/png"

            // From filename
            string contentType3 = MimeTypeHelper.GetContentTypeFromFilename("document.pdf"); // "application/pdf"
            string contentType4 = MimeTypeHelper.GetContentTypeFromFilename("video.mp4");    // "video/mp4"

            // Check if supported
            bool isSupported = MimeTypeHelper.IsSupported(".docx"); // true

            // Unknown extension returns default
            string unknown = MimeTypeHelper.GetContentType(".xyz"); // "application/octet-stream"
        }
    }
}
