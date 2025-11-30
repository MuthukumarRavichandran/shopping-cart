namespace Price.Calculator.Domain;

public class PriceRespository(IConfiguration configuration) : IPriceRespository
{
    private IConfiguration _configuration = configuration;

    /// <summary>
    /// Get all the price and discount for each item
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<RateAndDiscount>> GetPriceWithDiscount()
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("pricecalculatordb")))
        {
            var discountData = await connection.QueryAsync<RateAndDiscount>(
                "GetRateAndDiscount",
                commandType: CommandType.StoredProcedure
            );
            return discountData;
        }
    }
}
