using Protel.ExchangeRates.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protel.ExchangeRates.Services
{
    /// <summary>
    /// ExchangeRate service interface
    /// </summary>
    public partial interface IExchangeRateService
    {

        /// <summary>
        /// Gets all currencies
        /// </summary>
        /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the currencies
        /// </returns>
        Task<IList<ExchangeRate>> UpdateExchangeRatesAsync();

        /// <summary>
        /// Gets all currencies
        /// </summary>
        /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the currencies
        /// </returns>
        Task<ExchangeRate> GetExchangeRateOfCurrencyByDateAsync(string currencyCode, DateTime date);//https://www.tcmb.gov.tr/kurlar/201706/22062017.xml

        /// <summary>
        /// Gets all currencies
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the exchange rates
        /// </returns>
        Task<IList<ExchangeRate>> GetCurrentExchangeRatesAsync(string sortBy, bool orderAscending);


        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="currencyId">exchange rate identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the exchange rate
        /// </returns>
        Task<ExchangeRate> GetExchangeRateByIdAsync(int currencyId);


        /// <summary>
        /// Inserts currency
        /// </summary>
        /// <param name="exchangeRate">exchange rate</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertExchangeRateAsync(ExchangeRate exchangeRate);


        /// <summary>
        /// Inserts currencies
        /// </summary>
        /// <param name="exchangeRates">exchange rates</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertExchangeRateAsync(IList<ExchangeRate> exchangeRates);
    }
}
