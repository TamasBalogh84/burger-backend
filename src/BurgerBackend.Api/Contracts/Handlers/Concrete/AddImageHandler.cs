using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Results;
using BurgerBackend.Domain.Config;
using BurgerBackend.Domain.Repositories.Blob;
using Microsoft.Extensions.Options;

namespace BurgerBackend.Api.Contracts.Handlers.Concrete;

public class AddImageHandler : IAddImageHandler
{
    private readonly IImagesRepository _imagesRepository;

    private readonly ImageConfiguration _imageConfiguration;

    private readonly ILogger<AddImageHandler> _logger;

    public AddImageHandler(IImagesRepository imagesRepository, IOptions<ImageConfiguration> imageOptions, ILogger<AddImageHandler> logger)
    {
        _imagesRepository = imagesRepository ?? throw new ArgumentNullException(nameof(imagesRepository));
        _imageConfiguration = imageOptions.Value ?? throw new ArgumentNullException(nameof(imageOptions));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<AddImageResult> ExecuteAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(file.FileName) || file.Length == 0)
            {
                return AddImageResult.BadRequest("Invalid file.");
            }

            if (file.Length > _imageConfiguration.MaximumFileSizeInBytes)
            {
                return AddImageResult.BadRequest("File size is too large.");
            }

            var extension = Path.GetExtension(file.FileName);

            // TODO: extension based filtering

            var imageUrl = await _imagesRepository.UploadFileToStorage(file.OpenReadStream(), extension, cancellationToken);

            return AddImageResult.Ok(imageUrl);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return AddImageResult.InternalServerError(e.Message);
        }
    }
}