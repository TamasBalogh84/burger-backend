using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;

namespace BurgerBackend.Api.Contracts.Handlers.Abstract;

public interface IDeleteReviewHandler
{
    Task<DeleteReviewResult> ExecuteAsync(DeleteReviewParameters parameters, CancellationToken cancellationToken = default);
}