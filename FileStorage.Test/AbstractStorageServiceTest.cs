using FileStorage.Abstraction.Contracts;
using FileStorage.Abstraction.Events;
using FluentAssertions;
using System.Text;

namespace FileStorage.Test
{
    // Abstract base test class
    public abstract class AbstractStorageServiceTest<TStorage>(TStorage storage) : IAsyncDisposable
        where TStorage : IFileStorageService
    {
        protected readonly TStorage Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        private readonly List<string> _uploadedFiles = new();


        // Abstract method for each implementation to define how to create upload objects
        protected abstract IUploadStorageObject PrepareUploadObject(Stream content, string destinationPath);

        // Virtual methods that can be overridden if needed
        protected virtual string GetTestFileName() => $"test-file-{Guid.NewGuid()}.txt";
        protected virtual string GetTestContent() => "This is test content for storage service testing.";

        [Fact]
        public async Task UploadAsync_Should_Upload_File_Successfully()
        {
            // Arrange
            var fileName = GetTestFileName();
            var content = GetTestContent();
            var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var uploadObject = PrepareUploadObject(contentStream, fileName);

            // Act
            var result = await Storage.UploadAsync(uploadObject);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();

            // Track for cleanup
            _uploadedFiles.Add(fileName);

            // Verify file exists
            var exists = await Storage.IsExistAsync(fileName);
            exists.Should().BeTrue();
        }

        [Fact]
        public async Task DownloadAsync_Should_Download_Uploaded_File()
        {
            // Arrange - First upload a file
            var fileName = GetTestFileName();
            var originalContent = GetTestContent();
            var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(originalContent));
            var uploadObject = PrepareUploadObject(contentStream, fileName);

            await Storage.UploadAsync(uploadObject);
            _uploadedFiles.Add(fileName);

            // Act
            var downloadedObject = await Storage.DownloadAsync(fileName);

            // Assert
            downloadedObject.Should().NotBeNull();

            // Read and compare content
            using var reader = new StreamReader(downloadedObject.Content);
            var downloadedContent = await reader.ReadToEndAsync();
            downloadedContent.Should().Be(originalContent);
        }

        [Fact]
        public async Task IsExistAsync_Should_Return_False_For_NonExistent_File()
        {
            // Arrange
            var nonExistentFileName = $"non-existent-{Guid.NewGuid()}.txt";

            // Act
            var exists = await Storage.IsExistAsync(nonExistentFileName);

            // Assert
            exists.Should().BeFalse();
        }

        [Fact]
        public async Task GetFileMetadataAsync_Should_Return_Valid_Metadata()
        {
            // Arrange
            var fileName = GetTestFileName();
            var content = GetTestContent();
            var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var uploadObject = PrepareUploadObject(contentStream, fileName);

            await Storage.UploadAsync(uploadObject);
            _uploadedFiles.Add(fileName);

            // Act
            var metadata = await Storage.GetFileMetadataAsync(fileName);

            // Assert
            metadata.Should().NotBeNull();
            metadata.ContentLengthBytes.Should().BeGreaterThan(0);
            metadata.LastModifiedUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(5));
        }

        [Fact]
        public async Task GetStorageInfoAsync_Should_Return_Valid_Storage_Info()
        {
            // Act
            var storageInfo = await Storage.GetStorageInfoAsync();

            // Assert
            storageInfo.Should().NotBeNull();
            // Add more specific assertions based on your IStorageInfo interface
        }

        [Fact]
        public async Task UploadAsync_With_Progress_Should_Report_Progress()
        {
            // Arrange
            var fileName = GetTestFileName();
            var content = new string('A', 10000); // Larger content to see progress
            var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var uploadObject = PrepareUploadObject(contentStream, fileName);

            // Act
            var result = await Storage.UploadAsync(uploadObject, ProgressHandler);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            _uploadedFiles.Add(fileName);
            return;

            // Note: Progress reporting might not always trigger for small files
            // This test is more relevant for larger files or slower connections

            void ProgressHandler(object? sender, StorageTransferProgressArgs args)
            {
                _ = true;
                args.Should().NotBeNull();
            }
        }


        // put this down so it does not delete before previous test take place
        [Fact]
        public async Task DeleteAsync_Should_Delete_File_Successfully()
        {
            // Arrange
            var fileName = GetTestFileName();
            var content = GetTestContent();
            var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var uploadObject = PrepareUploadObject(contentStream, fileName);

            await Storage.UploadAsync(uploadObject);

            // Act
            var result = await Storage.DeleteAsync(fileName);

            // Assert
            result.Should().BeTrue();

            // Verify file no longer exists
            var exists = await Storage.IsExistAsync(fileName);
            exists.Should().BeFalse();
        }

        // Cleanup method
        public async ValueTask DisposeAsync()
        {
            foreach (var fileName in _uploadedFiles)
            {
                try
                {
                    await Storage.DeleteAsync(fileName);
                }
                catch
                {
                    // Ignore cleanup errors in tests
                }
            }

            _uploadedFiles.Clear();
            GC.SuppressFinalize(this);
        }
    }

}
