using System.Text;
using Azure.Storage.Blobs;
using BurgerBackend.Domain.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BurgerBackend.Domain.Repositories.Blob;

public class ImagesRepository : IImagesRepository
{
    private const string HttpPrefix = "https://";

    private const string BlobMainAddress = ".blob.core.windows.net/";

    private readonly BlobServiceClient _client;

    private readonly ILogger<ImagesRepository> _logger;

    private readonly AzureStorageConfiguration _storageConfig;

    public ImagesRepository(BlobServiceClient client, IOptions<AzureStorageConfiguration> storageOptions, ILogger<ImagesRepository> logger)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _storageConfig = storageOptions.Value ?? throw new ArgumentNullException(nameof(storageOptions));
    }

    public async Task<string> UploadFileToStorage(Stream fileStream, string fileExtension, CancellationToken cancellationToken)
    {
        string uniqueFileName = Guid.NewGuid() + "." + fileExtension;

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

        await containerClient.UploadBlobAsync(uniqueFileName, fileStream, cancellationToken);

        _logger.LogDebug($"File with name:{uniqueFileName} got uploaded.");

        return blobUri.ToString();
    }
}