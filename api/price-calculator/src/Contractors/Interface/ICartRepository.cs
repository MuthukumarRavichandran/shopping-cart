using Price.Calculator.Contractors.Model;

namespace Price.Calculator.Contracts.Interface;

public interface ICartRepository
{
    /// <summary>
    /// Get the cart data by sessionid
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<IEnumerable<Cart>> GetCartAsync(string userId);

    /// <summary>
    /// Save the item and quantity to cart
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="item"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    Task<bool> SaveCartAsync(string sessionId, string item, int quantity);
}
