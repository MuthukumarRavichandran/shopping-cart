using Microsoft.AspNetCore.Mvc;
using Price.Calculator.Contractors.Model;
using Price.Calculator.Contracts.Interface;
using Swashbuckle.AspNetCore.Annotations;

namespace Price.Calculator.Controllers;

/// <summary>
/// Price Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CartController(ICartService cartService) : ControllerBase
{
    private ICartService _cartService = cartService;

    /// <summary>
    /// Endpoint to retrive the items added to the cart with its price, discount and final price
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    [SwaggerOperation(
          Summary = "Retrieves a cart data for a session",
          Description = "Endpoint to retrive the items added to the cart with its price, discount and final price"
      )]
    [ProducesResponseType<CartDetailsWithPrice>(StatusCodes.Status200OK)]
    [ProducesResponseType<CartDetailsWithPrice>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(string sessionId, int quantity, string item)
    {
        try
        {
            var cartDetails = await _cartService.GetCartDetails(sessionId, quantity, item);
            return Ok(cartDetails);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}
