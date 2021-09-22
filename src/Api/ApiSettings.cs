using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Protel.ExchangeRates.API
{
    public static class Apisettings
    {
        public static IList<string> DEFAULT_CURRENCIES = new List<string>() {
            "USD",
            "EUR",
            "GBP",
            "CHF",
            "KWD",
            "SAR",
            "RUB"
        };
    }
}
