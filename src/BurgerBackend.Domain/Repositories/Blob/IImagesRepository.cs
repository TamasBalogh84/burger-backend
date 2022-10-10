namespace BurgerBackend.Domain.Repositories.Blob;

public interface IImagesRepository
{

    Task<string> UploadFileToStorage(Stream fileStream, string fileExtension, CancellationToken cancellationToken);
}