using Protel.ExchangeRates.Core.Domain;
using Protel.ExchangeRates.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protel.ExchangeRates.Services
{
    public class CurrencyService : ICurrencyService
    {
        #region Fields

        private readonly IRepository<ExchangeRate> _currencyRepository;

        #endregion

        #region Ctor

        public CurrencyService(IRepository<ExchangeRate> currencyReposityory)
        {
            _currencyRepository = currencyReposityory;
        }

        #endregion

        #region Methos

        public async Task<IList<ExchangeRate>> GetAllCurrenciesAsync()
        {
            return await _currencyRepository.GetAllAsync();
        }

        public async Task<ExchangeRate> GetCurrencyByIdAsync(int currencyId)
        {
            return await _currencyRepository.GetByIdAsync(currencyId);
        }

        public async Task InsertCurrencyAsync(ExchangeRate currency)
        {
            if (currency is null)
                throw new ArgumentNullException(nameof(currency));

            await _currencyRepository.InsertAsync(currency);
        }

        public async Task InsertCurrencyAsync(IList<ExchangeRate> currencies)
        {
            if (currencies is null)
                throw new ArgumentNullException(nameof(currencies));

            await _currencyRepository.InsertAsync(currencies);
        }

        #endregion
    }
}
