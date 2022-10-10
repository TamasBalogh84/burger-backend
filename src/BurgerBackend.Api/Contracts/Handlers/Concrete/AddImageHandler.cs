﻿using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Results;
using BurgerBackend.Domain.Repositories.Blob;

namespace BurgerBackend.Api.Contracts.Handlers.Concrete;

public class AddImageHandler : IAddImageHandler
{
    private const int BytesInTwoMegaBytes = 2097152;

    private readonly IImagesRepository _imagesRepository;

    private readonly ILogger<AddImageHandler> _logger;

    public AddImageHandler(IImagesRepository imagesRepository, ILogger<AddImageHandler> logger)
    {
        _imagesRepository = imagesRepository ?? throw new ArgumentNullException(nameof(imagesRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<AddImageResult> ExecuteAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        try
        {
            if (file.Length == 0)
            {
                return AddImageResult.BadRequest("Invalid file.");
            }

            if (file.Length >= BytesInTwoMegaBytes)
            {
                return AddImageResult.BadRequest("File size greater than 2MBs which is not allowed.");
            }

            var extension = Path.GetExtension(file.FileName);

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