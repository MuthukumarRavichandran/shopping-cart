using Price.Calculator.Contractors.Model;

namespace Price.Calculator.Contracts.Interface;

public interface ICartService
{
    /// <summary>
    /// Retrive all the cart details avaliable for the sessionId and save/update the item and quantity in cart
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="quantity"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    Task<IEnumerable<CartDetailsWithPrice>> GetCartDetails(string sessionId, int quantity, string item);
}
