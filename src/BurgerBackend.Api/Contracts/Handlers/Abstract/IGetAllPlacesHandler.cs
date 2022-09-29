using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;

namespace BurgerBackend.Api.Contracts.Handlers.Abstract;

public interface IGetAllPlacesHandler
{
    Task<GetAllPlacesResult> ExecuteAsync(GetAllPlacesParameters parameters, CancellationToken cancellationToken = default);
}