using NerdStore.Core.Messages.NerdStore.Core.Messages;
using System;

namespace NerdStore.Core.DomainObjects
{
    public class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateId)
            => AggregateId = aggregateId;
    }
}