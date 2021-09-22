using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Protel.ExchangeRates.Core
{
    public static class Constants
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
