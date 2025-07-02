# File Storage - AWS Implementation

This package provides an S3-compatible implementation of the `IFileStorageService`, suitable for Amazon S3, DigitalOcean Spaces, Backblaze B2, or any other provider that follows the S3 protocol.

It is part of the modular `FileStorage` ecosystem — designed for plug-and-play file storage across multiple backends.

---

## 📦 Dependencies

- Depends on: `FileStorage.Abstraction`, `FileStorage.Core`
- Used by: Apps or services integrating with S3-style object storage

---

## 🔌 Supported Implementations

Each S3-compatible provider has its own configuration class and registration helper.

### ✅ Available:

- [DigitalOcean Spaces](./ServiceProvider/DigitalOcean/README.md) — complete and production-ready

### 🛣️ Planned (Roadmap):

- Amazon S3 (native AWS SDK)
- Backblaze B2
- MinIO (local/dev usage)
- Wasabi Cloud Storage
- Alibaba OSS
- LocalDiskS3Proxy (for testing/mock environments)

> ⚠️ All of the above will follow the same base structure and config validation strategy, using `AwsProviderConfig` and `StorageServiceLifetime`.

---

## 📁 Implementation Details

This module provides:

- Generic S3 client abstraction using `AmazonS3Client`
- Flexible lifetime builder via `StorageServiceLifetime<T>`
- Base config model `AwsProviderConfig`, extendable per provider
- Safe defaults and config validation using `AwsProviderConfigDefault<T>`

You can build and register your own provider by inheriting from these classes, or use the built-in helpers.

---

## 📚 DigitalOcean Example

The full guide and usage instructions for DigitalOcean can be found here:

👉 [**DigitalOcean Implementation**](./ServiceProvider/DigitalOcean/README.md)

It shows how to:

- Configure the `DigitalOceanProviderConfig` in `appsettings.json`
- Register `AwsProviderConfigDefault<T>` for validation
- Inject the service using `.AddAwsDigitalOceanStorageProvider(...)`

---

## 🛠️ Contribution

PRs are welcome — but keep it clean:

- Shared logic must be useful across at least 2+ S3-compatible implementations
- Do not leak provider-specific logic into shared Core/Abstraction packages
- Keep DI, config, and usage minimal but flexible

---

## 📜 License

Licensed under the [MIT License](../../LICENSE).  
Use it to build, scale, or meme. Attribution optional, clean code expected.
