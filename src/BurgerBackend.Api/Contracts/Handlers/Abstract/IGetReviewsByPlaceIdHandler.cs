using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;

namespace BurgerBackend.Api.Contracts.Handlers.Abstract;

public interface IGetReviewsByPlaceIdHandler
{
    Task<GetReviewsByPlaceIdResult> ExecuteAsync(GetReviewsByPlaceIdParameters parameters, CancellationToken cancellationToken = default);
}