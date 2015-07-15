using MemBus;
using NEventStore;

namespace NEventStoreExample.Infrastructure
{
    public static class DelegateDispatcher
    {
        public static void DispatchCommit(IPublisher bus, ICommit commit)
        {
            foreach (var @event in commit.Events)
            {
                bus.Publish(@event.Body);
            }
        }
    }
}