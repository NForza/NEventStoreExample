using CommonDomain;
using System.Collections.Generic;

namespace NEventStoreExample.Infrastructure
{
    public static class IAggregateExtensions
    {
        public static void ApplyEvents( this IAggregate aggregate, IEnumerable<IEvent> events )
        {
            foreach (var ev in events)
                aggregate.ApplyEvent(ev);
        }
    }
}
