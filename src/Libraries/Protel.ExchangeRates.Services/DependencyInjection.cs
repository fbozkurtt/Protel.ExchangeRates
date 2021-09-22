﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Protel.ExchangeRates.Core.Events;
using Protel.ExchangeRates.Services.Events;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protel.ExchangeRates.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddScoped<ICurrencyService, CurrencyService>();

            services.AddHttpClient();

            return services;
        }
    }
}
