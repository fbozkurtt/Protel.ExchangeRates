using MediatR;
using Protel.ExchangeRates.Core.Infrastructure;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Protel.ExchangeRates.Core.Events.Handlers
{
    public class EntityInsertedEventHandler : INotificationHandler<DomainEventNotification<EntityInsertedEvent>>
    {
        public async Task Handle(DomainEventNotification<EntityInsertedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            Log.Information("Event occured: {DomainEvent}", domainEvent.GetType().Name);
        }

    }
}
