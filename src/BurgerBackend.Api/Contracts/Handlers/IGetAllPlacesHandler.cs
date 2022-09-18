using BurgerBackend.Api.Contracts;

namespace BurgerBackend.Domain.Handlers
{
    public interface IGetAllPlacesHandler
    {
        Task<GetAllPlacesResult> ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
