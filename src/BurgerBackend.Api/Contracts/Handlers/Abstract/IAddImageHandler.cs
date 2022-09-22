using BurgerBackend.Api.Contracts.Results;

namespace BurgerBackend.Api.Contracts.Handlers.Abstract;

public interface IAddImageHandler
{
    Task<AddImageResult> ExecuteAsync(IFormFile file, CancellationToken cancellationToken = default);
}