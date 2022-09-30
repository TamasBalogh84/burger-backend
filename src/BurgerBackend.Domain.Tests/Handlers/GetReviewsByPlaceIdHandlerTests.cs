using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Atc.Rest.FluentAssertions;
using BurgerBackend.Api.Contracts.Extensions;
using BurgerBackend.Api.Contracts.Handlers.Concrete;
using BurgerBackend.Api.Tests.TestData;
using BurgerBackend.Domain.Entities.Cosmos;
using BurgerBackend.Domain.Repositories.Cosmos;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BurgerBackend.Api.Tests.Handlers
{
    public class GetReviewsByPlaceIdHandlerTests
    {
        private readonly Mock<IBurgerPlacesRepository> _repositoryMock = new();

        private readonly Mock<ILogger<GetReviewsByPlaceIdHandler>> _loggerMock = new();

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
            var parameters = GetReviewsByPlaceIdParametersTestData.ValidGetReviewsByPlaceIdParameters;

            var data = testData.AsBurgerPlace();

            _repositoryMock.Setup(e => e.GetReviewsByPlaceIdAsync(parameters.PlaceId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(data.Reviews);

            var sut = new GetReviewsByPlaceIdHandler(_repositoryMock.Object, _loggerMock.Object);

            // ACT

            var result = await sut.ExecuteAsync(parameters, CancellationToken.None);

            // ASSERT
            result.Should().BeOkResult()
                .WithContent(data.Reviews.Select(r => r.ToReview()));
        }

        [TestCaseSource(typeof(BurgerPlaceTestData), nameof(BurgerPlaceTestData.MultipleReviewTestCase))]
        public async Task Should_Return_Ok_WithCorrectPagination(BurgerPlaceTestData testData)
        {
            // ARRANGE
            var parameters = GetReviewsByPlaceIdParametersTestData.ValidGetReviewsByPlaceIdParametersWithPagination;

            var data = testData.AsBurgerPlace();

            _repositoryMock.Setup(e => e.GetReviewsByPlaceIdAsync(parameters.PlaceId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(data.Reviews);

            var sut = new GetReviewsByPlaceIdHandler(_repositoryMock.Object, _loggerMock.Object);

            // ACT

            var result = await sut.ExecuteAsync(parameters, CancellationToken.None);

            // ASSERT
            var expectedReviews = data.Reviews.Select(r => r.ToReview()).Take(parameters.PageSize);

            result.Should().BeOkResult()
                .WithContent(expectedReviews);
        }

        [Test]
        public async Task Should_Return_BadRequest()
        {
            // ARRANGE
            var parameters = GetReviewsByPlaceIdParametersTestData.InValidGetReviewsByPlaceIdParameters;

            var sut = new GetReviewsByPlaceIdHandler(_repositoryMock.Object, _loggerMock.Object);

            // ACT
            var result = await sut.ExecuteAsync(parameters, CancellationToken.None);

            // ASSERT
            result.Should().BeBadRequestResult().WithErrorMessage("Invalid Guid!");
        }

        [Test]
        public async Task Should_Return_NotFound()
        {
            // ARRANGE
            var parameters = GetReviewsByPlaceIdParametersTestData.GetReviewsByPlaceIdParametersWithNotExistingPlace;

            _repositoryMock.Setup(e => e.GetReviewsByPlaceIdAsync(parameters.PlaceId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Enumerable.Empty<Review>());

            var sut = new GetReviewsByPlaceIdHandler(_repositoryMock.Object, _loggerMock.Object);

            // ACT
            var result = await sut.ExecuteAsync(parameters, CancellationToken.None);

            // ASSERT
            result.Should().BeNotFoundResult().WithErrorMessage($"No burger place with Id {parameters.PlaceId} found!");
        }

        [Test]
        public async Task Should_Return_InternalServerError()
        {
            // ARRANGE
            var parameters = GetReviewsByPlaceIdParametersTestData.ValidGetReviewsByPlaceIdParameters;

            _repositoryMock.Setup(e => e.GetReviewsByPlaceIdAsync(parameters.PlaceId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var sut = new GetReviewsByPlaceIdHandler(_repositoryMock.Object, _loggerMock.Object);

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
