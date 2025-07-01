# 🧾 FileStorage

Reusable, modular AF file storage abstraction for .NET projects.  
Need to upload, download, or delete files without hardcoding S3, Web APIs, or local paths?  
This is your plug.

Built for real engineers who hate writing the same storage code across 5 projects.

---

## 🔧 Features

- Clean interfaces for upload/download/delete
- Pluggable implementations: AWS S3, Web API, Local disk
- Support for progress tracking via events
- Easy to extend with your own provider
- Written with composability and testability in mind

---

## 📁 Project Structure

📦 FileStorage
├── Abstraction                     # Interfaces and shared contracts
├── Core                            # Shared logic and utilities
├── Implementation.Aws             # AWS S3 implementation
├── Implementation.LocalFileSystem # Local disk-based implementation
├── Implementation.WebApi          # Upload to backend via HTTP
└── Test                            # Unit and integration tests

---

## 🚀 Usage (Example)

```csharp

var storage = new AwsFileStorageService(...);
await storage.UploadAsync(new UploadRequest
{
    Stream = myStream,
    FileName = "image.jpg",
    Path = "images/uploads/"
});


```

---

You can also hook into progress:

```csharp
await storage.DownloadAsync("images/uploads/image.jpg",
    onDownloadProgressChanged: (s, e) =>
    {
        Console.WriteLine($"Progress: {e.PercentDone}%");
    });

```

---

### 🧩 With Dependency Injection

Each implementation comes with its own setup for dependency injection.  
Just follow the registration steps provided in that specific package (e.g., `AddAwsFileStorage()`, `AddWebApiFileStorage()`, etc.).

No magic. No bloated frameworks. Just clean DI.

---

## 🧪 Implementations

| Project                                      | Description                                 |
| -------------------------------------------- | ------------------------------------------- |
| `FileStorage.Implementation.Aws`             | Upload to and download from AWS S3          |
| `FileStorage.Implementation.WebApi`          | Communicates with a remote backend          |
| `FileStorage.Implementation.LocalFileSystem` | Ideal for dev environments or simple setups |

---

## 🛠️ Roadmap
- Basic API

- Upload progress event

- Azure Blob Storage implementation

- Streaming downloads

- Optional caching

---

## 🧾 License

This project is licensed under the MIT License.
Use it. Extend it. Abuse it (ethically).

---

## 🤝 Contributing

If you write good code, open a PR.
If you write garbage, open a PR and we’ll fix it together.
If you're a vibe coder... go away.

## 🧠 Why Tho?
Because real projects deserve real engineering—not spaghetti cloud SDK wrappers duct-taped to the controller layer.
This lib is part of a larger mission to build reliable, testable, low-maintenance infrastructure pieces for .NET apps that scale clean.

Made with blood, caffeine, and zero tolerance for bullshit.

— Tio x Rancid