using Price.Calculator.Contractors.Model;

namespace Price.Calculator.Contracts.Interface;

public interface IPriceRespository
{
    /// <summary>
    /// Get all the price and discount for each item
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<RateAndDiscount>> GetPriceWithDiscount();
}
