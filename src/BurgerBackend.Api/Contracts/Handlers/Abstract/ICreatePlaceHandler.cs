using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;

namespace BurgerBackend.Api.Contracts.Handlers.Concrete;

public interface ICreatePlaceHandler
{
    Task<CreatePlaceResult> ExecuteAsync(CreatePlaceParameters parameters, CancellationToken cancellationToken = default);
}