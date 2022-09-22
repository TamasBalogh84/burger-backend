namespace BurgerBackend.Domain.Repositories.Blob;

public interface IImagesRepository
{

    Task<string> UploadFileToStorage(Stream fileStream, string fileName, CancellationToken cancellationToken);
}