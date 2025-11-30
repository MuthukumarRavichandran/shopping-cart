using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Price.Calculator.Contractors.Model;
using Price.Calculator.Contracts.Interface;
using Price.Calculator.Controllers;

namespace Api.Tests
{
    public class CartControllerTests
    {
        private readonly Mock<ICartService> _mockCartService;
        private readonly CartController _controller;
        private Fixture _autoFixture;

        public CartControllerTests()
        {
            _mockCartService = new Mock<ICartService>();
            _controller = new CartController(_mockCartService.Object);
            _autoFixture = new Fixture();
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenServiceReturnsData()
        {
            var sessionId = _autoFixture.Create<string>();
            var itemInRequest = "apple";
            var item1 = _autoFixture.Build<CartDetailsWithPrice>()
                .With(x => x.Item, itemInRequest).Create();
            var item2 = _autoFixture.Build<CartDetailsWithPrice>()
                .With(x => x.Item, "orange").Create();
            _mockCartService
                .Setup(s => s.GetCartDetails(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync([item1, item2]);

            var result = await _controller.Get(sessionId, 3, itemInRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<CartDetailsWithPrice>>(okResult.Value);
            Assert.Contains(returnValue, x => x.Item == "apple");
        }

        [Fact]
        public async Task Get_ReturnsBadRequest_WhenServiceThrowsException()
        {
            _mockCartService
                .Setup(s => s.GetCartDetails(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Something went wrong"));

            var result = await _controller.Get(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>());

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Something went wrong", badRequestResult.Value);
        }
    }
}
