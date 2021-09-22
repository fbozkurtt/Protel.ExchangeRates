using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Protel.ExchangeRates.Core;
using Protel.ExchangeRates.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Protel.ExchangeRates.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CurrencyController : BaseController
    {
        #region Fiels

        private readonly IExchangeRateService _exchangeRateService;

        #endregion

        #region Ctor

        public CurrencyController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        #endregion

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetCurrentExchangeRates(string sortBy = "rate", bool orderAscending = true)
        {
            var result = (await _exchangeRateService.GetCurrentExchangeRatesAsync(sortBy, orderAscending)).
                Select(_ => $"TRY/{_.Kod} {_.ForexBuying ?? _.BanknoteBuying}");

            return new JsonResult(new
            {
                success = true,
                message = "",
                result
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetExchangeRateByDate(string currencyCode, string date)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                throw new ArgumentNullException(nameof(currencyCode));

            if (string.IsNullOrWhiteSpace(date))
                throw new ArgumentNullException(nameof(date));

            if (!Constants.DEFAULT_CURRENCIES.Contains(currencyCode.ToUpper()))
                return new JsonResult(new
                {
                    success = false,
                    message = $"Currency code must be one of the following codes: {string.Join(", ", Constants.DEFAULT_CURRENCIES)}"
                })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

            if (DateTime.TryParseExact(date, "dd-MM-yyyy", provider: null, style: System.Globalization.DateTimeStyles.None, out var exchangeRateDate))
            {
                //await _exchangeRateService.GetExchangeRateOfCurrencyByDateAsync(currencyCode, exchangeRateDate);
                var result = await _exchangeRateService.GetExchangeRateOfCurrencyByDateAsync(currencyCode, exchangeRateDate);

                return new JsonResult(new
                {
                    success = true,
                    message = "",
                    result = result is null ? string.Empty : $"TRY/{result.Kod} {exchangeRateDate.ToString("dd.MM.yyyy")} {result.ForexBuying ?? result.BanknoteBuying}"
                });
            }

            else
            {
                return new JsonResult(new
                {
                    success = false,
                    message = $"Date must be in the format of \"dd-MM-yyyy\"."
                })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }
    }

    #endregion
}
