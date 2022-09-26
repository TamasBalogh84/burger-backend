using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Atc.Rest.FluentAssertions;
using BurgerBackend.Api.Contracts.Extensions;
using BurgerBackend.Api.Contracts.Handlers.Concrete;
using BurgerBackend.Api.Tests.TestData;
using BurgerBackend.Domain.Repositories.Cosmos;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using BurgerPlace = BurgerBackend.Domain.Entities.Cosmos.BurgerPlace;

namespace BurgerBackend.Api.Tests.Handlers
{
    public class CreateReviewHandlerTests
    {
        private readonly Mock<IBurgerPlacesRepository> _repositoryMock = new();

        private readonly Mock<ILogger<CreateReviewHandler>> _loggerMock = new();

        [SetUp]
        public void Setup()
        {
            _repositoryMock.Reset();
            _loggerMock.Reset();
        }

        [TestCaseSource(typeof(BurgerPlaceTestData), nameof(BurgerPlaceTestData.HappyPathTestCases))]
        public async Task Should_Return_Ok(BurgerPlaceTestData testData)
        {
            // ARRANGE
            var parameters = CreateReviewParametersTestData.OkCreateReviewParameters;

            var data = testData.AsBurgerPlace();

            _repositoryMock.Setup(e => e.GetByIdAsync(parameters.PlaceId.ToString(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(data);

            data.Reviews = data.Reviews.Append(parameters.Review.ToReview());

            _repositoryMock.Setup(e => e.StoreAsync(data, It.IsAny<CancellationToken>()))
                .ReturnsAsync(data);

            var sut = new CreateReviewHandler(_repositoryMock.Object, _loggerMock.Object);

            // ACT

            var result = await sut.ExecuteAsync(parameters, CancellationToken.None);

            // ASSERT
            result.Should().BeOkResult()
                .WithContent(parameters.Review);
        }

        [Test]
        public async Task Should_Return_BadRequest()
        {
            // ARRANGE
            var parameters = CreateReviewParametersTestData.OkCreateReviewParameters;

            parameters.PlaceId = Guid.Empty;

            var sut = new CreateReviewHandler(_repositoryMock.Object, _loggerMock.Object);

            // ACT
            var result = await sut.ExecuteAsync(parameters, CancellationToken.None);

            // ASSERT
            result.Should().BeBadRequestResult().WithErrorMessage("Invalid parameters!");
        }

        [Test]
        public async Task Should_Return_NotFound()
        {
            // ARRANGE
            var parameters = CreateReviewParametersTestData.OkCreateReviewParameters;

            _repositoryMock.Reset();

            _repositoryMock.Setup(e => e.GetByIdAsync(parameters.PlaceId.ToString(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((BurgerPlace?)null);

            var sut = new CreateReviewHandler(_repositoryMock.Object, _loggerMock.Object);

            // ACT
            var result = await sut.ExecuteAsync(parameters, CancellationToken.None);

            // ASSERT
            result.Should().BeNotFoundResult().WithErrorMessage($"Burger place not found with id: {parameters.PlaceId}");
        }

        [Test]
        public async Task Should_Return_InternalServerError()
        {
            // ARRANGE
            var parameters = CreateReviewParametersTestData.OkCreateReviewParameters;

            _repositoryMock.Setup(e => e.GetByIdAsync(parameters.PlaceId.ToString(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var sut = new CreateReviewHandler(_repositoryMock.Object, _loggerMock.Object);

            // ACT
            var result = await sut.ExecuteAsync(parameters, CancellationToken.None);

            // ASSERT
            result
                .Should()
                .BeContentResult()
                .WithStatusCode(HttpStatusCode.InternalServerError);
        }
    }
}
