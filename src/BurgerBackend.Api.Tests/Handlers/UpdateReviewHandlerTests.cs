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
    public class UpdateReviewHandlerTests
    {
        private readonly Mock<IBurgerPlacesRepository> _repositoryMock = new();

        private readonly Mock<ILogger<UpdateReviewHandler>> _loggerMock = new();

        [SetUp]
        public void Setup()
        {
            _repositoryMock.Reset();
            _loggerMock.Reset();
        }

        [TestCaseSource(typeof(BurgerPlaceTestData), nameof(BurgerPlaceTestData.MultipleReviewTestCase))]
        public async Task Should_Return_Ok(BurgerPlaceTestData testData)
        {
            // ARRANGE
            var parameters = UpdateReviewParametersTestData.ValidUpdateReviewParameters;

            var data = testData.AsBurgerPlace();

            _repositoryMock.Setup(e => e.GetByIdAsync(parameters.PlaceId.ToString(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(data);

            _repositoryMock.Setup(e => e.ReplaceAsync(data, parameters.PlaceId.ToString(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var sut = new UpdateReviewHandler(_repositoryMock.Object, _loggerMock.Object);

            // ACT

            var result = await sut.ExecuteAsync(parameters, CancellationToken.None);

            // ASSERT
            result.Should().BeOkResult()
                .WithContent(true);
        }

        [Test]
        public async Task Should_Return_BadRequest()
        {
            // ARRANGE
            var parameters = UpdateReviewParametersTestData.InvalidUpdateReviewParameters;

            var sut = new UpdateReviewHandler(_repositoryMock.Object, _loggerMock.Object);

            // ACT
            var result = await sut.ExecuteAsync(parameters, CancellationToken.None);

            // ASSERT
            result.Should().BeBadRequestResult().WithErrorMessage("Invalid Guid!");
        }

        [Test]
        public async Task Should_Return_NotFound()
        {
            // ARRANGE
            var parameters = UpdateReviewParametersTestData.NotExistingUpdateReviewParameters;

            _repositoryMock.Reset();

            _repositoryMock.Setup(e => e.GetByIdAsync(parameters.PlaceId.ToString(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((BurgerPlace?)null);

            var sut = new UpdateReviewHandler(_repositoryMock.Object, _loggerMock.Object);

            // ACT
            var result = await sut.ExecuteAsync(parameters, CancellationToken.None);

            // ASSERT
            result.Should().BeNotFoundResult().WithErrorMessage($"No data found to update PlaceID: {parameters.PlaceId} ReviewID: {parameters.ReviewId}");
        }

        [Test]
        public async Task Should_Return_InternalServerError()
        {
            // ARRANGE
            var parameters = UpdateReviewParametersTestData.ValidUpdateReviewParameters;

            _repositoryMock.Setup(e => e.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var sut = new UpdateReviewHandler(_repositoryMock.Object, _loggerMock.Object);

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
