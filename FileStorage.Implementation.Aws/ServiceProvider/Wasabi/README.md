
# 🔌 Usage

To integrate this package in your app:

### 1. 🧾 Add config to your `appsettings.json`

```json
"Wasabi": {
  "ServiceUrl": "your service url",
  "BucketName": "your-bucket",
  "SignatureVersion": "4",
  "ForcePathStyle": true,
  "StorageKey": "wasabi",
  "AccessKeyIdEnvironmentName": "your-access-key-env",
  "AccessKeySecretEnvironmentName": "your-secret-key-env"
}

```

Make sure the actual credentials (AccessKeyId, AccessKeySecret) are stored securely in your environment variables — never hardcoded.

---

### 2. 🧩 Configure it in your Program.cs or Startup.cs

```csharp
// Register the config section
builder.Services.Configure<WasabiProviderConfig>(
    builder.Configuration.GetSection("Wasabi"));

```

---

### 3. 🧠 Register the actual storage implementation

```csharp
// Inject as concrete or interface, keyed or not — you decide
builder.Services.AddWasabiStorageProvider(service =>
    service.AsKeyedSingletonOfConcrete("wasabi"));
```
You can also do:

```csharp
service.AsSingleton()                  // default IFileStorageService
service.AsSingletonOfConcrete()       // register using concrete type
service.AsKeyedSingleton("my-key")    // register IFileStorageService under a DI key
```

---

### ✅ After that...

You're good to go. Inject `IFileStorageService` or the concrete class into your services and you can start using:

```csharp
await fileStorage.UploadAsync(...);
await fileStorage.DownloadAsync(...);
```

Just make sure you resolve the correct one if you’re using keyed DI.

### 🧠 Tips
- The provider reads credentials from environment variables (not appsettings.json) based on the config’s AccessKeyIdEnvironmentName and AccessKeySecretEnvironmentName.

- You can configure multiple storage buckets by defining multiple config sections and registering multiple instances (keyed).


### 🛠️ Contribution

Feel free to extend logic here only if:

   - It is independent of a specific storage provider

   - Useful across two or more S3-based implementations

   - Doesn't add unnecessary abstraction or bloat

### 📜 License

Licensed under the MIT License.
Use it commercially, privately, or to troll vibe coders. Attribution optional, satisfaction guaranteed.
