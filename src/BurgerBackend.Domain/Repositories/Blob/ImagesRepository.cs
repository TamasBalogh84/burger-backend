using System.Text;
using Azure.Storage;
using Azure.Storage.Blobs;
using BurgerBackend.Domain.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BurgerBackend.Domain.Repositories.Blob;

public class ImagesRepository : IImagesRepository
{
    private readonly BlobServiceClient _client;

    private readonly ILogger<ImagesRepository> _logger;

    private const string HttpPrefix = "https://";

    private const string BlobMainAddress = ".blob.core.windows.net/";

    private readonly AzureStorageConfiguration _storageConfig;

    public ImagesRepository(BlobServiceClient client, IOptions<AzureStorageConfiguration> storageOptions, ILogger<ImagesRepository> logger)
    {
        _client = client;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _storageConfig = storageOptions.Value ?? throw new ArgumentNullException(nameof(storageOptions));
    }

    public async Task<string> UploadFileToStorage(Stream fileStream, string fileName, CancellationToken cancellationToken)
    {
        string uniqueFileName = Guid.NewGuid() + "_" + fileName;

        var stringBuilder = new StringBuilder();

        stringBuilder
            .Append(HttpPrefix)
            .Append(_storageConfig.AccountName)
            .Append(BlobMainAddress)
            .Append(_storageConfig.ImageContainerName)
            .Append("/")
            .Append(uniqueFileName);

        var containerClient = _client.GetBlobContainerClient(_storageConfig.ImageContainerName);

        Uri blobUri = new(stringBuilder.ToString());

        StorageSharedKeyCredential storageCredentials = new(_storageConfig.AccountName, _storageConfig.AccountKey);

        await containerClient.UploadBlobAsync(uniqueFileName, fileStream, cancellationToken);

        return blobUri.ToString();
    }
}