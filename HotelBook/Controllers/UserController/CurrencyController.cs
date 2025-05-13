using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers.CurrencyExchange;

namespace HotelBook.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyExchangeService _currencyExchangeService;

        public CurrencyController(CurrencyExchangeService currencyExchangeService)
        {
            _currencyExchangeService = currencyExchangeService;
        }

        [HttpGet("convert")]
        public async Task<IActionResult> Convert(decimal amount, string from,string to)
        {
            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
                return BadRequest("Currency codes are required.");

            var converted = await _currencyExchangeService.ConvertAsync(amount, from.ToUpper(), to.ToUpper());
            return Ok( converted );
        }
    }
}
