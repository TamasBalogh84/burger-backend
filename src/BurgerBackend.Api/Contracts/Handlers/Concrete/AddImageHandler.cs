using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Results;
using BurgerBackend.Domain.Repositories.Blob;

namespace BurgerBackend.Api.Contracts.Handlers.Concrete;

public class AddImageHandler : IAddImageHandler
{
    private readonly IImagesRepository _imagesRepository;

    public AddImageHandler(IImagesRepository imagesRepository)
    {
        _imagesRepository = imagesRepository;
    }

    public async Task<AddImageResult> ExecuteAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var imageUrl = await _imagesRepository.UploadFileToStorage(file.OpenReadStream(), file.FileName, cancellationToken);

        return AddImageResult.Ok(imageUrl);
    }
}