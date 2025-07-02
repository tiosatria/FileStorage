
using HeyRed.Mime;

namespace FileStorage.Core
{
    public static class Utilities
    {
        public static string GuessContentType(Stream stream)
            => MimeGuesser.GuessMimeType(stream);

        public static string GuessContentType(string fullPath)
        => MimeGuesser.GuessMimeType(fullPath);

        public static string GuessExtension(string fullPath) 
        => MimeGuesser.GuessMimeType(fullPath);

        public static string GuessExtension(Stream stream)
            => MimeGuesser.GuessExtension(stream);

        public static long GetContentLength(Stream stream)
        => stream.Length;

        public static (string fileName, string fileExt, string contentType) GetBaseFileInfoFromPath(string fullPath)
        => (Path.GetFileName(fullPath), Path.GetExtension(fullPath), MimeGuesser.GuessMimeType(fullPath)
            );

        /// <summary>
        /// Make sure you call this from local, or else will throw error
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileInfo GetLocalFileInfo(string path)
            => new FileInfo(path);



    }
}
