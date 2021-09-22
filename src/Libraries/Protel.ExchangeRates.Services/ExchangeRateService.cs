using Newtonsoft.Json;
using Protel.ExchangeRates.Core;
using Protel.ExchangeRates.Core.Domain;
using Protel.ExchangeRates.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Protel.ExchangeRates.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        #region Fields

        private readonly IRepository<ExchangeRate> _currencyRepository;
        private readonly IHttpClientFactory _clientFactory;

        #endregion

        #region Ctor

        public ExchangeRateService(IRepository<ExchangeRate> currencyRepository, IHttpClientFactory clientFactory)
        {
            _currencyRepository = currencyRepository;
            _clientFactory = clientFactory;
        }

        #endregion

        #region Methos


        public async Task<IList<ExchangeRate>> GetCurrentExchangeRatesAsync(string sortBy = "rate", bool orderAscending = true)
        {
            var exchangeRates = (await _currencyRepository.GetAllAsync())
                .Where(_ => _.Created.Date == DateTime.Now.Date)
                .GroupBy(_ => _.Kod)
                .Select(_ => _.Last());

            if (exchangeRates.Count() < Constants.DEFAULT_CURRENCIES.Count())
            {
                using (var client = _clientFactory.CreateClient("TCMB"))
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, "today.xml");
                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        using var responseStream = await response.Content.ReadAsStreamAsync();

                        StreamReader reader = new StreamReader(responseStream);
                        var result = reader.ReadToEnd();

                        var xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(result);

                        XmlNodeList currencies = xmlDocument.GetElementsByTagName("Currency");

                        foreach (XmlNode currency in currencies)
                        {
                            if (Constants.DEFAULT_CURRENCIES.Contains(currency.Attributes["Kod"].Value.ToUpper()))
                            {
                                var serializedXmlNode = JsonConvert.SerializeXmlNode(
                                currency,
                                Newtonsoft.Json.Formatting.Indented,
                                true);

                                var exchangeRate = JsonConvert.DeserializeObject<ExchangeRate>(serializedXmlNode);

                                exchangeRate.CrossOrder = Convert.ToInt32(currency.Attributes["CrossOrder"].Value);
                                exchangeRate.Kod = currency.Attributes["Kod"].Value;
                                exchangeRate.CurrencyCode = currency.Attributes["CurrencyCode"].Value;
                                exchangeRate.ExchangeRateDate = DateTime.TryParseExact(xmlDocument["Tarih_Date"].GetAttribute("Tarih"), "dd.MM.yyyy", provider: null, style: System.Globalization.DateTimeStyles.None, out var exchangeRateDate) ? exchangeRateDate : null;
                                exchangeRates = exchangeRates.Append(exchangeRate);

                                await _currencyRepository.InsertAsync(exchangeRate);
                            }
                        }

                        //if (exchangeRates.Any())
                        //    await _currencyRepository.InsertAsync(exchangeRates.ToArray());
                    }
                }
            }


            if (sortBy.ToUpper().Equals("RATE"))
            {
                if (orderAscending)
                    exchangeRates = exchangeRates.OrderBy(_ => _.ForexBuying);
                else
                    exchangeRates = exchangeRates.OrderByDescending(_ => _.ForexBuying);
            }
            else
            {
                if (orderAscending)
                    exchangeRates = exchangeRates.OrderBy(_ => _.Kod);
                else
                    exchangeRates = exchangeRates.OrderByDescending(_ => _.Kod);
            }


            return exchangeRates.ToList();

        }

        public async Task<ExchangeRate> GetExchangeRateByIdAsync(int currencyId)
        {
            return await _currencyRepository.GetByIdAsync(currencyId);
        }

        public async Task<ExchangeRate> GetExchangeRateOfCurrencyByDateAsync(string currencyCode, DateTime date)
        {
            var exchangeRate = (await _currencyRepository.GetAllAsync()).Where(_ => _.ExchangeRateDate?.Date == date.Date).LastOrDefault();

            if (exchangeRate is null)
            {
                using (var client = _clientFactory.CreateClient("TCMB"))
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, $"{date.ToString("yyyyMM")}/{date.ToString("ddMMyyyy")}.xml");
                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        using var responseStream = await response.Content.ReadAsStreamAsync();

                        StreamReader reader = new StreamReader(responseStream);
                        var result = reader.ReadToEnd();

                        var xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(result);

                        XmlNodeList currencies = xmlDocument.GetElementsByTagName("Currency");

                        foreach (XmlNode currency in currencies)
                        {
                            if (currency.Attributes["Kod"].Value.ToUpper() == currencyCode.ToUpper())
                            {
                                var serializedXmlNode = JsonConvert.SerializeXmlNode(
                                    currency,
                                    Newtonsoft.Json.Formatting.Indented,
                                    true);

                                exchangeRate = JsonConvert.DeserializeObject<ExchangeRate>(serializedXmlNode);

                                exchangeRate.CrossOrder = Convert.ToInt32(currency.Attributes["CrossOrder"].Value);
                                exchangeRate.Kod = currency.Attributes["Kod"].Value;
                                exchangeRate.CurrencyCode = currency.Attributes["CurrencyCode"].Value;
                                exchangeRate.ExchangeRateDate = date;

                                await _currencyRepository.InsertAsync(exchangeRate);
                            }
                        }
                    }
                }
            }

            return exchangeRate;
        }

        public async Task InsertExchangeRateAsync(ExchangeRate exchangeRate)
        {
            if (exchangeRate is null)
                throw new ArgumentNullException(nameof(exchangeRate));

            await _currencyRepository.InsertAsync(exchangeRate);
        }

        public async Task InsertExchangeRateAsync(IList<ExchangeRate> exchangeRates)
        {
            if (exchangeRates is null)
                throw new ArgumentNullException(nameof(exchangeRates));

            await _currencyRepository.InsertAsync(exchangeRates);
        }

        public Task<IList<ExchangeRate>> UpdateExchangeRatesAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
