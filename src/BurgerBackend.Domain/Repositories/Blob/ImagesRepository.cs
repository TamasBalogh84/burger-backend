using Azure.Storage;
using Azure.Storage.Blobs;
using BurgerBackend.Domain.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BurgerBackend.Domain.Repositories.Blob;

public class ImagesRepository : IImagesRepository
{
    private readonly AzureStorageConfiguration _storageConfig;

    public ImagesRepository(IOptions<AzureStorageConfiguration> storageOptions)
    {
        _storageConfig = storageOptions.Value;
    }

    public async Task<string> UploadFileToStorage(Stream fileStream, string fileName, CancellationToken cancellationToken)
    {
        string uniqueFileName = Guid.NewGuid() + "_" + fileName;

        // Create a URI to the blob
        Uri blobUri = new Uri("https://" +
                              _storageConfig.AccountName +
                              ".blob.core.windows.net/" +
                              _storageConfig.ImageContainer +
                              "/" + uniqueFileName);

        StorageSharedKeyCredential storageCredentials =
            new StorageSharedKeyCredential(_storageConfig.AccountName, _storageConfig.AccountKey);

        BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

        await blobClient.UploadAsync(fileStream);

        return blobUri.ToString();
    }
}