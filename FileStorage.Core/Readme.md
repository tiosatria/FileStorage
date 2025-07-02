# File Storage - Core

This is the **core module** of the File Storage library. It contains **shared logic**, **utilities**, and **base abstractions** used across different storage implementations (e.g. AWS S3, Web API, Local Disk).

This project is **not tied to any specific provider**. Its purpose is to provide reusable components that help unify behavior and reduce duplication across all `FileStorage.Implementation.X` modules.

---

## 🧱 What's Inside

- Common value objects (e.g. `FileUploadRequest`, `FileUploadResult`)
- Utility classes (e.g. `PathHelper`, `MimeTypeResolver`)
- Default behaviors (optional fallback logic, e.g. basic validation, retry policies)
- Shared exceptions (e.g. `FileStorageException`)

---

## 📦 Dependencies

- Depends on: `FileStorage.Abstraction`, `Microsoft.Extensions.DependecyInjection`
- Used by: All implementation modules (`FileStorage.Implementation.Aws`, `FileStorage.Implementation.WebApi`, etc.)

---

## 🔌 Usage

This module is not intended to be used directly. Instead, it is consumed by concrete implementations. For example:

```csharp
services.AddAwsFileStorage(); // under the hood uses core logic + abstractions

```

## 🛠️ Contribution

Feel free to extend shared logic here only if it's:

    Independent of any storage provider

    Useful across two or more implementations

    Doesn't introduce unnecessary complexity

## 📜 License

This project is licensed under the [MIT License](./LICENSE).  
Feel free to use it in commercial or personal projects. Attribution appreciated, but not required.
