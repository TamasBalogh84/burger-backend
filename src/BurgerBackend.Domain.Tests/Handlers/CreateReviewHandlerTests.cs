using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Atc.Rest.FluentAssertions;
using Atc.Test;
using AutoFixture.Xunit2;
using BurgerBackend.Api.Contracts.Extensions;
using BurgerBackend.Api.Contracts.Handlers.Concrete;
using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Domain.Entities.Cosmos;
using BurgerBackend.Domain.Repositories.Cosmos;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace BurgerBackend.Api.Tests.Handlers
{
    public class CreateReviewHandlerTests
    {
        [Theory, AutoNSubstituteData]
        public async Task Should_Return_Ok(
            [Frozen] IBurgerPlacesRepository repository,
            CreateReviewParameters parameters,
            CreateReviewHandler sut,
            BurgerPlace place,
            CancellationToken cancellationToken)
        {
            // Arrange

            var updatePlace = new BurgerPlace
            {
                AvailableBurgers = place.AvailableBurgers,
                Id = place.Id,
                Information = place.Information,
                Location = place.Location,
                OpeningTime = place.OpeningTime,
                PartitionKey = place.PartitionKey,
                Reviews = place.Reviews.Append(parameters.Review.ToReview())
            };

            repository
                .GetByIdAsync(parameters.PlaceId.ToString(), cancellationToken)
                .ReturnsForAnyArgs(place);

            repository.StoreAsync(updatePlace).ReturnsForAnyArgs(updatePlace);

            // Act
            var actual = await sut.ExecuteAsync(parameters, cancellationToken);

            // Assert
            await repository
                .Received(1)
                .StoreAsync(place,Arg.Any<CancellationToken>());

            actual
                .Should()
                .BeOkResult()
                .WithContent(parameters.Review);
        }

        [Theory, AutoNSubstituteData]
        public async Task Should_Return_BadRequest(
            [Frozen] IBurgerPlacesRepository repository,
            CreateReviewParameters parameters,
            CreateReviewHandler sut,
            CancellationToken cancellationToken)
        {
            // Arrange
            parameters.PlaceId = Guid.Empty;

            // Act
            var actual = await sut.ExecuteAsync(parameters, cancellationToken);

            // Assert
            await repository
                .DidNotReceive()
                .StoreAsync(Arg.Any<BurgerPlace>(), Arg.Any<CancellationToken>());

            actual
                .Should()
                .BeBadRequestResult();
        }

        [Theory, AutoNSubstituteData]
        public async Task Should_Return_NotFound(
            [Frozen] IBurgerPlacesRepository repository,
            CreateReviewParameters parameters,
            CreateReviewHandler sut,
            CancellationToken cancellationToken)
        {
            // Arrange
            repository
                .GetByIdAsync(parameters.PlaceId.ToString(), cancellationToken)
                .ReturnsForAnyArgs((Task<BurgerPlace?>)null);

            // Act
            var actual = await sut.ExecuteAsync(parameters, cancellationToken);

            // Assert
            await repository
                .DidNotReceive()
                .StoreAsync(Arg.Any<BurgerPlace>(), Arg.Any<CancellationToken>());

            actual
                .Should()
                .BeNotFoundResult()
                .WithErrorMessage($"Burger place not found with id: {parameters.PlaceId}");
        }

        [Theory]
        public async Task Should_Return_InternalServerError(
            [Frozen] IBurgerPlacesRepository repository,
            CreateReviewParameters parameters,
            CreateReviewHandler sut,
            CancellationToken cancellationToken)
        {
            // Arrange
            repository
                .GetByIdAsync(Arg.Any<string>(), cancellationToken)
                .ThrowsForAnyArgs<Exception>();

            // Act
            var actual = await sut.ExecuteAsync(parameters, cancellationToken);

            // Assert
            actual
                .Should()
                .BeContentResult()
                .WithStatusCode(HttpStatusCode.InternalServerError);
        }
    }
}
