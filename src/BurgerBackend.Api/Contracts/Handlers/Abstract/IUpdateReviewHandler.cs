using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;

namespace BurgerBackend.Api.Contracts.Handlers.Abstract;

public interface IUpdateReviewHandler
{
    Task<UpdateReviewResult> ExecuteAsync(UpdateReviewParameters parameters, CancellationToken cancellationToken = default);
}