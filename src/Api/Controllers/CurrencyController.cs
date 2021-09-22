using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Protel.ExchangeRates.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCurrencies(string sortBy = "code", bool orderAscending = true) 
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrencyByDate(string currencyCode, string date)
        {
            if(string.IsNullOrWhiteSpace(currencyCode))
                throw new ArgumentNullException(nameof(currencyCode));

            if (string.IsNullOrWhiteSpace(date))
                throw new ArgumentNullException(nameof(date));

            if (!Apisettings.DEFAULT_CURRENCIES.Contains(currencyCode))
                return new JsonResult(new
                {
                    success = false,
                    message = $"ExchangeRate code must be one of the following codes: {string.Join(", ", Apisettings.DEFAULT_CURRENCIES)}"
                })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

            return new JsonResult(new
            {
                success = true,
                message = $"ExchangeRate code must be one of the following codes: {string.Join(", ", Apisettings.DEFAULT_CURRENCIES)}"
            });
        }
    }
}
