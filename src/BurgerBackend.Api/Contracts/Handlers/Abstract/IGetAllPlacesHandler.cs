using BurgerBackend.Api.Contracts.Results;

namespace BurgerBackend.Api.Contracts.Handlers.Abstract
{
    public interface IGetAllPlacesHandler
    {
        Task<GetAllPlacesResult> ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
