using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Xunit;

namespace Restaurents.API.Middleware.Tests
{
    public class ErrorExceptionHandlingTests
    {
        [Fact()]
        public async Task InvokeAsyncTest_WhenNoException_InvokeNext()
        {
            var logger = new Mock<ILogger<ErrorExceptionHandling>>();

            var httpContext = new DefaultHttpContext();
            var nextDelegate = new Mock<RequestDelegate>();

            var exceptionHandler = new ErrorExceptionHandling(logger.Object);
            
            await exceptionHandler.InvokeAsync(httpContext, nextDelegate.Object);

            nextDelegate.Verify(next => next.Invoke(httpContext), Times.Once);
        }

        [Fact()]
        public async Task InvokeAsyncTest_WhenNotFoundException_ReturnStatusCode404()
        {
            var logger = new Mock<ILogger<ErrorExceptionHandling>>();
            var httpContext = new DefaultHttpContext();

            var notFoundException = new NotFoundException(nameof(Restaurant), "1");

            var exceptionHandler = new ErrorExceptionHandling(logger.Object);

            await exceptionHandler.InvokeAsync(httpContext, _ => throw notFoundException);

            httpContext.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact()]
        public async Task InvokeAsyncTest_WhenForbidException_ReturnStatusCode403()
        {
            var logger = new Mock<ILogger<ErrorExceptionHandling>>();
            var httpContext = new DefaultHttpContext();

            var exceptionHandler = new ErrorExceptionHandling(logger.Object);

            await exceptionHandler.InvokeAsync(httpContext, _ => throw new ForbidException());

            httpContext.Response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
        }

        [Fact()]
        public async Task InvokeAsyncTest_WhenException_ReturnStatusCode500()
        {
            var logger = new Mock<ILogger<ErrorExceptionHandling>>();
            var httpContext = new DefaultHttpContext();

            var exceptionHandler = new ErrorExceptionHandling(logger.Object);

            await exceptionHandler.InvokeAsync(httpContext, _ => throw new Exception());

            httpContext.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}