using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Protel.ExchangeRates.Core;
using Protel.ExchangeRates.Core.Domain;
using Protel.ExchangeRates.Core.Infrastructure;
using Protel.ExchangeRates.Data;
using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Protel.ExchangeRates.Services.Jobs
{
    [DisallowConcurrentExecution]
    class UpdateExchangeRatesJob : IJob
    {
        #region Fields

        private readonly IExchangeRateService _exchangeRateService;

        #endregion

        #region Ctor

        public UpdateExchangeRatesJob(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        #endregion

        #region Methods

        public async Task Execute(IJobExecutionContext context)
        {
            Log.Information("Updating exchange rates from the remote server");

            await _exchangeRateService.UpdateExchangeRatesAsync();
        }

        #endregion
    }
}
