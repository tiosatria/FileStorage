# File Storage - Abstraction

This library is focused **purely on abstraction** for file storage functionality.  
It defines clean and stable interfaces for operations like file upload, download, deletion, and metadata retrieval.

There are **no dependencies** on specific SDKs, platforms, or providers. It is designed to be **extremely lightweight and implementation-agnostic**, allowing multiple storage backends to be plugged in seamlessly.

---

## 📦 What's Inside

- Contracts such as:
  - `IFileStorageService`
  - `IStorageFileMetadata`
  - `IFileUploadResult`
  - `IStorageInfo`
  - `IUploadStorageObject`
  - `IStorageObject`
- Events: 
    -  `StorageTransferProgressArgs`
- Common enums like:
  - `FileVisibilityEnum`
- Basic abstractions that ensure consistency across all storage providers.
---

## 🔁 Implementations

For actual usage, pair this abstraction layer with one of the concrete implementations:

- [`FileStorage.Implementation.WebApi`](../FileStorage.Implementation.WebApi)
- [`FileStorage.Implementation.Aws`](../FileStorage.Implementation.Aws)

You can also build your own provider by implementing the interfaces defined here.

---

## 🤝 Dependency Guidelines

- This project should **never depend on** any external SDK or implementation.
- Implementation projects should reference this library, not the other way around.

---

## 📜 License

This project is licensed under the [MIT License](./LICENSE).  
Feel free to use it in commercial or personal projects. Attribution appreciated, but not required.
