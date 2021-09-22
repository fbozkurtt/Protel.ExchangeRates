using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Protel.ExchangeRates.Core.Domain
{
    public partial class Currency : BaseEntity
    {
        public int CrossOrder { get; set; }
        public string Kod { get; set; }
        public string CurrencyCode { get; set; }
        public short Unit { get; set; }
        public string Name { get; set; }
        public string CurrencyName { get; set; }
        public decimal ForexBuying { get; set; }
        public decimal ForexSelling { get; set; }
        public decimal BanknoteBuying { get; set; }
        public decimal BanknoteSelling { get; set; }
        public decimal CrossRateUSD { get; set; }
        public decimal CrossRateOther { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
