using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;

namespace BurgerBackend.Api.Contracts.Handlers.Abstract;

public interface ICreateReviewHandler
{
    Task<CreateReviewResult> ExecuteAsync(CreateReviewParameters parameters, CancellationToken cancellationToken = default);
}