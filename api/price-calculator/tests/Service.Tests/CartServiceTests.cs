using Moq;
using Price.Calculator.Contractors.Model;
using Price.Calculator.Contracts.Interface;
using Price.Calculator.Service;

namespace Service.Tests;

public class CartServiceTests
{
    private readonly Mock<ICartRepository> _cartRepo;
    private readonly Mock<IPriceRespository> _priceRepo;
    private readonly CartService _cartService;

    public CartServiceTests()
    {
        _cartRepo = new Mock<ICartRepository>();
        _priceRepo = new Mock<IPriceRespository>();
        _cartService = new CartService(_cartRepo.Object, _priceRepo.Object);
    }

    [Fact]
    public async Task ShouldThrowException_WhenSaveFails()
    {
        _cartRepo
            .Setup(r => r.SaveCartAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(false);

        var ex = await Assert.ThrowsAsync<Exception>(() =>
            _cartService.GetCartDetails("abc", 2, "orange"));

        Assert.Equal("Not able to save in the cart. Please try again", ex.Message);
    }


    [Fact]
    public async Task ShouldReturnEmptyList_WhenCartIsEmpty()
    {
        _cartRepo
            .Setup(r => r.SaveCartAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(true);
        _cartRepo
            .Setup(r => r.GetCartAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<Cart>());

        var result = await _cartService.GetCartDetails("abc", 1, "apple");

        Assert.Empty(result);
    }

    [Fact]
    public async Task ShouldReturnCalculatedPrice_WhenCartExists()
    {
        var sessionId = "abc";
        var item = "banana";
        var quantity = 3;
        _cartRepo
            .Setup(r => r.SaveCartAsync(sessionId, item, quantity))
            .ReturnsAsync(true);
        _cartRepo
            .Setup(r => r.GetCartAsync(sessionId))
            .ReturnsAsync(new List<Cart>
            {
                new Cart
                {
                    ItemName = "banana",
                    Quantity = quantity
                }
            });
        _priceRepo
            .Setup(r => r.GetPriceWithDiscount())
            .ReturnsAsync(new List<RateAndDiscount>
            {
                new RateAndDiscount
                {
                    Item = "banana",
                    Discount = 10,
                    Rate = 50
                }
            });

        var result = await _cartService.GetCartDetails(sessionId, quantity, item);

        Assert.Single(result);
        var itemResult = result.First();
        Assert.Equal("banana", itemResult.Item);
        Assert.Equal(3, itemResult.Quantity);
        Assert.Equal(50, itemResult.Rate);
        Assert.Equal(10, itemResult.Discount);
        Assert.Equal(135m, itemResult.Price);
    }
}
