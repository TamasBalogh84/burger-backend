using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;

namespace BurgerBackend.Api.Contracts.Handlers.Abstract;

public interface IGetPlaceByIdHandler
{
    Task<GetPlayceByIdResult> ExecuteAsync(GetPlaceByIdParameters parameters, CancellationToken cancellationToken = default);
}