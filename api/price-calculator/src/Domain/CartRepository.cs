namespace Price.Calculator.Domain;

public class CartRepository(IConfiguration configuration) : ICartRepository
{
    private IConfiguration _configuration = configuration;

    /// <summary>
    /// Get the cart data by sessionid
    /// </summary>
    /// <param name="sessionId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Cart>> GetCartAsync(string sessionId)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("pricecalculatordb")))
        {
            var parameters = new { sessionId };
            var Cart = await connection.QueryAsync<Cart>(
                "GetCartBySessionId",
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return Cart;
        }
    }

    /// <summary>
    /// Save the item and quantity to cart
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="item"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public async Task<bool> SaveCartAsync(string sessionId, string item, int quantity)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("pricecalculatordb")))
        {
            var parameters = new { sessionId, item, quantity };
            var responseStatus = await connection.ExecuteAsync(
                "SaveCart",
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return responseStatus == 1;
        }
    }
}
