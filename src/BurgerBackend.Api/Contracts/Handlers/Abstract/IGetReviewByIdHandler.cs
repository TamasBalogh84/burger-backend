using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;

namespace BurgerBackend.Api.Contracts.Handlers.Abstract;

public interface IGetReviewByIdHandler
{
    Task<GetReviewByIdResult> ExecuteAsync(GetReviewByIdParameters parameters, CancellationToken cancellationToken = default);
}