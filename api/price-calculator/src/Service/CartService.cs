using Price.Calculator.Contractors.Model;
using Price.Calculator.Contracts.Interface;

namespace Price.Calculator.Service;

public class CartService(ICartRepository cartRepository, IPriceRespository priceRespository) : ICartService
{
    private ICartRepository _cartRepository = cartRepository;
    private IPriceRespository _priceRespository = priceRespository;

    /// <summary>
    /// Retrive all the cart details avaliable for the sessionId and save/update the item and quantity in cart
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="quantity"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CartDetailsWithPrice>> GetCartDetails(string sessionId, int quantity, string item)
    {
       var saveToCartStatus =  await _cartRepository.SaveCartAsync(sessionId, item, quantity);
        if (!saveToCartStatus)
            throw new Exception("Not able to save in the cart. Please try again");

        var exisitingCart = await _cartRepository.GetCartAsync(sessionId);
        if(exisitingCart == null || !exisitingCart.Any())
        {
            return [];
        }
        var priceWithDiscountData = await _priceRespository.GetPriceWithDiscount();

        var response = new List<CartDetailsWithPrice>();
        foreach(var cart in exisitingCart)
        {
            var discount = priceWithDiscountData.FirstOrDefault(x => x.Item == cart.ItemName);
            decimal totalprice = cart.Quantity * discount.Rate;
            decimal discountedPrice = totalprice * (discount.Discount / 100m);
            response.Add(new CartDetailsWithPrice
            {
                Item = cart.ItemName,
                Quantity = cart.Quantity,
                Rate = discount.Rate,
                Discount = discount.Discount,
                Price = totalprice - discountedPrice
            });
        }
        return response;
    }
}
