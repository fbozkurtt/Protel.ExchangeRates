using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protel.ExchangeRates.Core.Events
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
