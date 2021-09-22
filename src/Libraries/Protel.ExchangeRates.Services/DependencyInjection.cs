using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Protel.ExchangeRates.Core.Events;
using Protel.ExchangeRates.Services.Events;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protel.ExchangeRates.Services.Jobs;
using Protel.ExchangeRates.Data;
using Quartz.Spi;
using Quartz;
using Quartz.Impl;
using Protel.ExchangeRates.Core.Domain;

namespace Protel.ExchangeRates.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("TCMB", c =>
            {
                c.BaseAddress = new Uri("https://www.tcmb.gov.tr/kurlar/");
                c.DefaultRequestHeaders.Add("Accept", "application/xml");
            });
            services.AddHostedService<QuartzHostedService>();

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddScoped<IExchangeRateService, ExchangeRateService>();

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<UpdateExchangeRatesJob>();

            services.AddSingleton(new JobScheduler(
                jobType: typeof(UpdateExchangeRatesJob),
                cronExpression: "0/10 * * ? * * *"));//0 0 9-18 ? * MON-FRI *

            return services;
        }
    }
}
