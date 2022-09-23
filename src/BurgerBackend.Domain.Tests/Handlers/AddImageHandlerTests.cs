using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Atc.Rest.FluentAssertions;
using Atc.Test;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using BurgerBackend.Api.Contracts.Handlers.Concrete;
using BurgerBackend.Domain.Repositories.Blob;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace BurgerBackend.Api.Tests.Handlers;

public class AddImageHandlerTests
{
    [Theory]
    public void Should_Throw_For_Null_ImagesRepository(ILogger<AddImageHandler> logger)
        => Assert.Throws<ArgumentNullException>(() => new AddImageHandler(null, logger));

    [Theory]
    public void Should_Throw_For_Null_Logger(IImagesRepository imagesRepository)
        => Assert.Throws<ArgumentNullException>(()
            => new AddImageHandler(imagesRepository, null));


    [Theory, AutoNSubstituteData]
    public async Task Should_Return_Ok(
        [Frozen] IImagesRepository repository,
        IFormFile file,
        AddImageHandler sut,
        CancellationToken cancellationToken)
    {
        // Arrange
        var fileName = file.FileName;

        repository
            .UploadFileToStorage(Arg.Any<Stream>(), fileName, cancellationToken)
            .ReturnsForAnyArgs("dummyUrl");

        // Act
        var actual = await sut.ExecuteAsync(file, cancellationToken);

        // Assert
        await repository
            .Received(1)
            .UploadFileToStorage(file.OpenReadStream(), fileName, Arg.Any<CancellationToken>());

        actual
            .Should()
            .BeOkResult();
    }

    [Theory, AutoNSubstituteData]
    public async Task Should_Return_BadRequest(
        [Frozen] IImagesRepository repository,
        IFormFile file,
        AddImageHandler sut,
        CancellationToken cancellationToken)
    {
        // Arrange
        var fileName = file.FileName;



        repository
            .UploadFileToStorage(Arg.Any<Stream>(), fileName, cancellationToken)
            .ReturnsForAnyArgs("dummyUrl");

        // Act
        var actual = await sut.ExecuteAsync(file, cancellationToken);

        // Assert
        await repository
            .Received(1)
            .UploadFileToStorage(file.OpenReadStream(), fileName, Arg.Any<CancellationToken>());

        actual
            .Should()
            .BeOkResult();
    }

    [Theory]
    public async Task Should_Return_InternalServerError(
        [Frozen] IImagesRepository repository,
        IFormFile file,
        AddImageHandler sut,
        CancellationToken cancellationToken)
    {
        // Arrange
        var fileName = file.FileName;

        repository
            .UploadFileToStorage(Arg.Any<Stream>(), fileName, cancellationToken)
            .ThrowsForAnyArgs<Exception>();

        // Act
        var actual = await sut.ExecuteAsync(file, cancellationToken);

        // Assert
        actual
            .Should()
            .BeContentResult()
            .WithStatusCode(HttpStatusCode.InternalServerError);
    }

}